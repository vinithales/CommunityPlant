using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskEntity = CommunityPlant.Domain.Entities.Task;

namespace CommunityPlant.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> CreateAsync(TaskEntity task);

        Task<TaskEntity?> GetByIdAsync(int taskId);

        Task<IEnumerable<TaskEntity>> GetByGardenIdAsync(int gardenId);
        Task<bool> UpdateAsync(TaskEntity task);
    }
}