using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using CommunityPlant.Domain.Interfaces;

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


    


        async Task<TaskResponseDTO> ITaskService.CreateTaskAsync(CreateTaskDTO taskData, GardenDTO gardenDto)
        {
            if(string.IsNullOrEmpty(taskData.Name) && string.IsNullOrEmpty(taskData.Description) && string.IsNullOrEmpty()){
                throw new ArgumentException("O nome da Tarefa é obrigatório.");
            }

        
            var garden = await _gardenRepository.GetGardenByIdAsync(gardenDto.Id);
            if(garden == null){
                throw new KeyNotFoundException("Jardim não encontrado.");
            }

            var task = _mapper.Map<Task>(taskData);

            var createdTask = await _taskRepository.CreateAsync(task);
            
            return _mapper.Map<TaskResponseDTO>(createdTask);



        }

        async Task<bool> ITaskService.CompleteTaskAsync(int taskId)
        {
            
            throw new NotImplementedException();
        }

        async Task<TaskResponseDTO> ITaskService.GetTaskByIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<TaskResponseDTO>> ITaskService.GetTasksByGardenIdAsync(int gardenId)
        {
            throw new NotImplementedException();
        }
    }
}