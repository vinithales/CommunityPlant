using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IPlantedCropService
    {
        Task<PlantedCropResponseDTO> CreatePlantedCropAsync(CreatePlantedCropDTO plantedCropDto);
        Task<PlantedCropResponseDTO?> GetPlantedCropByIdAsync(int id);
        Task<List<PlantedCropResponseDTO>> GetPlantedCropsByGardenAsync(int gardenId);
        Task<List<PlantedCropResponseDTO>> GetPlantedCropsByUserAsync(int userId);
        Task<List<PlantedCropResponseDTO>> GetReadyToHarvestAsync();
        Task<PlantedCropResponseDTO> UpdatePlantedCropStatusAsync(int id, string status);
        Task<PlantedCropResponseDTO> HarvestCropAsync(int id);
        Task<bool> DeletePlantedCropAsync(int id);
    }
}