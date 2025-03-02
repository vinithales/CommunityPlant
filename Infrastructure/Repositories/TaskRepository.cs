using System;
using System.Collections.Generic;
using System.Linq;
using CommunityPlant.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Task = CommunityPlant.Domain.Entities.Task;

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

        public async Task<Domain.Entities.Task> CreateAsync(Domain.Entities.Task task)
        {

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> GetByIdAsync(int taskId)
        {

            var task = await _context.Tasks.Include(t => t.Garden)
                                    .FirstOrDefaultAsync(t => t.Id == taskId);

            return task;
        }

        public async Task<IEnumerable<Task>> GetByGardenIdAsync(int gardenId)
        {

            var existingTask = await _context.Tasks.FindAsync(gardenId);
            if(existingTask == null){
                throw new Exception("Jardim não encontrado.");
            }

            var tasks = await _context.Tasks.Where(t => t.GardenId == gardenId).ToListAsync();

            return tasks;
        }
        public async Task<bool> UpdateAsync(Task task)
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