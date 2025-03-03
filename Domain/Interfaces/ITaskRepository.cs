using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Task> CreateAsync (Task task);

        Task<Task> GetByIdAsync(int taskId);

        Task<IEnumerable<Task>> GetByGardenIdAsync(int gardenId);
        Task<bool> UpdateAsync(Task task);
        

    }
}