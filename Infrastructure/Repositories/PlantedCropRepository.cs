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
    public class PlantedCropRepository : IPlantedCropRepository
    {
        private readonly AppDbContext _context;

        public PlantedCropRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PlantedCrop> CreatePlantedCropAsync(PlantedCrop plantedCrop)
        {
            // Calculate expected harvest date based on plant's days to harvest
            var plant = await _context.Plants.FindAsync(plantedCrop.PlantId);
            if (plant != null)
            {
                plantedCrop.ExpectedHarvestDate = plantedCrop.PlantedDate.AddDays(plant.DaysToHarvest);
            }

            _context.PlantedCrops.Add(plantedCrop);
            await _context.SaveChangesAsync();
            return plantedCrop;
        }

        public async Task<PlantedCrop?> GetPlantedCropByIdAsync(int id)
        {
            return await _context.PlantedCrops
                .Include(pc => pc.Garden)
                .Include(pc => pc.Plant)
                .Include(pc => pc.PlantedByUser)
                .FirstOrDefaultAsync(pc => pc.Id == id && pc.IsActive);
        }

        public async Task<List<PlantedCrop>> GetPlantedCropsByGardenAsync(int gardenId)
        {
            return await _context.PlantedCrops
                .Include(pc => pc.Plant)
                .Include(pc => pc.PlantedByUser)
                .Where(pc => pc.GardenId == gardenId && pc.IsActive)
                .OrderBy(pc => pc.PlantedDate)
                .ToListAsync();
        }

        public async Task<List<PlantedCrop>> GetPlantedCropsByUserAsync(int userId)
        {
            return await _context.PlantedCrops
                .Include(pc => pc.Garden)
                .Include(pc => pc.Plant)
                .Where(pc => pc.PlantedByUserId == userId && pc.IsActive)
                .OrderBy(pc => pc.PlantedDate)
                .ToListAsync();
        }

        public async Task<List<PlantedCrop>> GetPlantedCropsByStatusAsync(string status)
        {
            return await _context.PlantedCrops
                .Include(pc => pc.Garden)
                .Include(pc => pc.Plant)
                .Include(pc => pc.PlantedByUser)
                .Where(pc => pc.Status == status && pc.IsActive)
                .OrderBy(pc => pc.PlantedDate)
                .ToListAsync();
        }

        public async Task<List<PlantedCrop>> GetReadyToHarvestAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.PlantedCrops
                .Include(pc => pc.Garden)
                .Include(pc => pc.Plant)
                .Include(pc => pc.PlantedByUser)
                .Where(pc => pc.IsActive && 
                           pc.ExpectedHarvestDate.HasValue && 
                           pc.ExpectedHarvestDate.Value.Date <= today &&
                           pc.Status != "Harvested")
                .OrderBy(pc => pc.ExpectedHarvestDate)
                .ToListAsync();
        }

        public async Task<PlantedCrop> UpdatePlantedCropAsync(PlantedCrop plantedCrop)
        {
            _context.PlantedCrops.Update(plantedCrop);
            await _context.SaveChangesAsync();
            return plantedCrop;
        }

        public async Task<bool> DeletePlantedCropAsync(int id)
        {
            var plantedCrop = await _context.PlantedCrops.FindAsync(id);
            if (plantedCrop == null) return false;

            plantedCrop.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PlantedCrops
                .AnyAsync(pc => pc.Id == id && pc.IsActive);
        }
    }
}