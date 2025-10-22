using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IPlantRepository
    {
        Task<Plant> CreatePlantAsync(Plant plant);
        Task<Plant?> GetPlantByIdAsync(int id);
        Task<List<Plant>> GetAllPlantsAsync();
        Task<List<Plant>> GetPlantsByTypeAsync(string type);
        Task<List<Plant>> SearchPlantsAsync(string searchTerm);
        Task<Plant> UpdatePlantAsync(Plant plant);
        Task<bool> DeletePlantAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}