using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IGardenService
    {
        Task<GardenResponseDTO> CreateGardenAsync(CreateGardenDTO gardenDto);
        Task<GardenResponseDTO?> GetGardenByIdAsync(int id);
        Task<List<GardenResponseDTO>> GetAllGardensAsync();
        Task<List<GardenResponseDTO>> GetPublicGardensAsync();
        Task<List<GardenResponseDTO>> GetGardensByUserAsync(int userId);
        Task<GardenResponseDTO> UpdateGardenAsync(int id, GardenDTO gardenDto);
        Task<bool> DeleteGardenAsync(int id);
    }
}