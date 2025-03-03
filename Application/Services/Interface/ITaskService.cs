using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface ITaskService
    {
        async Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO taskData, GardenDTO gardenDto);
        async Task<TaskResponseDTO> GetTaskByIdAsync(int taskId);
        async Task<IEnumerable<TaskResponseDTO>> GetTasksByGardenIdAsync(int gardenId);
        async Task<bool> CompleteTaskAsync(int taskId);
    
    }
}