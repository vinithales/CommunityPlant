using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityPlant.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly AppDbContext _context;

        public PlantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Plant> CreatePlantAsync(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
            return plant;
        }

        public async Task<Plant?> GetPlantByIdAsync(int id)
        {
            return await _context.Plants
                .Include(p => p.PlantedCrops)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<List<Plant>> GetAllPlantsAsync()
        {
            return await _context.Plants
                .AsNoTracking()
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<List<Plant>> GetPlantsByTypeAsync(string type)
        {
            return await _context.Plants
                .AsNoTracking()
                .Where(p => p.IsActive && p.Type == type)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<List<Plant>> SearchPlantsAsync(string searchTerm)
        {
            return await _context.Plants
                .AsNoTracking()
                .Where(p => p.IsActive && 
                           (p.Name.Contains(searchTerm) || 
                            p.ScientificName.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm)))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Plant> UpdatePlantAsync(Plant plant)
        {
            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();
            return plant;
        }

        public async Task<bool> DeletePlantAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return false;

            plant.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Plants
                .AnyAsync(p => p.Id == id && p.IsActive);
        }
    }
}
