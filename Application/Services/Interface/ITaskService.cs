using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Application.Services.Interface
{
    public interface ITaskService
    {
        Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO taskData);
        Task<TaskResponseDTO> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskResponseDTO>> GetTasksByGardenIdAsync(int gardenId);
        Task<bool> CompleteTaskAsync(int taskId);
        Task CreateTaskAsync(object taskDto);
    }
}