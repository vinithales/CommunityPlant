using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IParticipationService
    {
        Task<ParticipationResponseDTO> JoinGardenAsync(CreateParticipationDTO participationDto);
        Task<ParticipationResponseDTO?> GetParticipationByIdAsync(int id);
        Task<List<ParticipationResponseDTO>> GetParticipationsByGardenAsync(int gardenId);
        Task<List<ParticipationResponseDTO>> GetParticipationsByUserAsync(int userId);
        Task<ParticipationResponseDTO> UpdateParticipationRoleAsync(int id, string role);
        Task<bool> LeaveGardenAsync(int userId, int gardenId);
        Task<bool> UserCanParticipateInGardenAsync(int userId, int gardenId);
    }
}