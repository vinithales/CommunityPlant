using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IParticipationRepository
    {
        Task<Participation> CreateParticipationAsync(Participation participation);
        Task<Participation?> GetParticipationByIdAsync(int id);
        Task<List<Participation>> GetParticipationsByGardenAsync(int gardenId);
        Task<List<Participation>> GetParticipationsByUserAsync(int userId);
        Task<Participation?> GetActiveParticipationAsync(int userId, int gardenId);
        Task<Participation> UpdateParticipationAsync(Participation participation);
        Task<bool> DeleteParticipationAsync(int id);
        Task<bool> UserParticipatesInGardenAsync(int userId, int gardenId);
    }
}