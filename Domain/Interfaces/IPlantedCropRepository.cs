using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IPlantedCropRepository
    {
        Task<PlantedCrop> CreatePlantedCropAsync(PlantedCrop plantedCrop);
        Task<PlantedCrop?> GetPlantedCropByIdAsync(int id);
        Task<List<PlantedCrop>> GetPlantedCropsByGardenAsync(int gardenId);
        Task<List<PlantedCrop>> GetPlantedCropsByUserAsync(int userId);
        Task<List<PlantedCrop>> GetPlantedCropsByStatusAsync(string status);
        Task<List<PlantedCrop>> GetReadyToHarvestAsync();
        Task<PlantedCrop> UpdatePlantedCropAsync(PlantedCrop plantedCrop);
        Task<bool> DeletePlantedCropAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}