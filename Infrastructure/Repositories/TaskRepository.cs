using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskEntity = CommunityPlant.Domain.Entities.Task;

namespace CommunityPlant.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly AppDbContext _context;

        public TaskRepository(
            AppDbContext context
        )
        {
            _context = context;
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity?> GetByIdAsync(int taskId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(t => t.Garden)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<TaskEntity>> GetByGardenIdAsync(int gardenId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.GardenId == gardenId)
                .ToListAsync();

        }
        
        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
