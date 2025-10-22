using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IGardenRepository
    {
        Task<Garden> CreateGardenAsync(Garden garden);
        Task<Garden?> GetGardenByIdAsync(int id);
        Task<List<Garden>> GetAllGardensAsync();
        Task<List<Garden>> GetPublicGardensAsync();
        Task<List<Garden>> GetGardensByUserAsync(int userId);
        Task<Garden> UpdateGardenAsync(Garden garden);
        Task<bool> DeleteGardenAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}