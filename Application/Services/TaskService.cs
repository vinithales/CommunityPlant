using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Application.Services.Interface;

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


    



        

        Task<TaskResponseDTO> ITaskService.CreateTaskAsync(CreateTaskDTO taskData)
        {
            if(string.IsNullOrEmpty(taskData.Name)){
                throw new ArgumentException("O nome da Tareda é obrigatório.");
            }

            var garden = await _gardenRepository.GetTaskByIdAsync(TaskData);
            if(garden == null){
                throw new KeyNotFoundException("Jardim não encontrado.");
            }

            var task = _mapper.Map<Task>(taskData);
            task.Status = "Pending";

            var createdTask = await _taskRepository.CreateAsync(task);
            
            return _mapper.Map<TaskResponseDTO>(createdTask);



        }

        Task<bool> ITaskService.CompleteTaskAsync(int taskId)
        {
            
            throw new NotImplementedException();
        }

        Task<TaskResponseDTO> ITaskService.GetTaskByIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TaskResponseDTO>> ITaskService.GetTasksByGardenIdAsync(int gardenId)
        {
            throw new NotImplementedException();
        }
    }
}