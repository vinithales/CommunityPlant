using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IPlantService
    {
        Task<PlantResponseDTO> CreatePlantAsync(CreatePlantDTO plantDto);
        Task<PlantResponseDTO?> GetPlantByIdAsync(int id);
        Task<List<PlantResponseDTO>> GetAllPlantsAsync();
        Task<List<PlantResponseDTO>> GetPlantsByTypeAsync(string type);
        Task<List<PlantResponseDTO>> SearchPlantsAsync(string searchTerm);
        Task<PlantResponseDTO> UpdatePlantAsync(int id, PlantDTO plantDto);
        Task<bool> DeletePlantAsync(int id);
    }
}