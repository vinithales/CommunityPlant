using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using CommunityPlant.Domain.Interfaces;
using TaskEntity = CommunityPlant.Domain.Entities.Task;

namespace CommunityPlant.Application.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IGardenRepository _gardenRepository;
        private readonly IMapper _mapper;


        public TaskService(
            ITaskRepository taskRepository,
            IGardenRepository gardenRepository,
            IMapper mapper
        ){
            _taskRepository = taskRepository;
            _gardenRepository = gardenRepository;
            _mapper = mapper;
        }


    


        public async Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO taskData)
        {
            if (string.IsNullOrWhiteSpace(taskData.Name))
            {
                throw new ArgumentException("O nome da Tarefa é obrigatório.");
            }

            var garden = await _gardenRepository.GetGardenByIdAsync(taskData.GardenId);
            if (garden == null)
            {
                throw new KeyNotFoundException("Jardim não encontrado.");
            }

            var task = _mapper.Map<TaskEntity>(taskData);
            task.CreatedAt = DateTime.UtcNow;

            var createdTask = await _taskRepository.CreateAsync(task);
            return _mapper.Map<TaskResponseDTO>(createdTask);
        }

        public async Task<bool> CompleteTaskAsync(int taskId)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(taskId);
            if (taskEntity == null)
                return false;

            taskEntity.Status = "Completed";
            taskEntity.CompletedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(taskEntity);
            return true;
        }

        public async Task<TaskResponseDTO> GetTaskByIdAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            return _mapper.Map<TaskResponseDTO>(task);
        }

        public async Task<IEnumerable<TaskResponseDTO>> GetTasksByGardenIdAsync(int gardenId)
        {
            var tasks = await _taskRepository.GetByGardenIdAsync(gardenId);
            return _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);
        }
    }
}