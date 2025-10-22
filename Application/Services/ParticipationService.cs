using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Interfaces;

namespace CommunityPlant.Application.Services
{
    public class ParticipationService : IParticipationService
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGardenRepository _gardenRepository;
        private readonly IMapper _mapper;

        public ParticipationService(
            IParticipationRepository participationRepository,
            IUserRepository userRepository,
            IGardenRepository gardenRepository,
            IMapper mapper)
        {
            _participationRepository = participationRepository;
            _userRepository = userRepository;
            _gardenRepository = gardenRepository;
            _mapper = mapper;
        }

        public async Task<ParticipationResponseDTO> JoinGardenAsync(CreateParticipationDTO participationDto)
        {
            // Validate user exists
            var user = await _userRepository.GetUserByIdAsync(participationDto.UserId);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado.");

            // Validate garden exists
            var garden = await _gardenRepository.GetGardenByIdAsync(participationDto.GardenId);
            if (garden == null)
                throw new ArgumentException("Jardim não encontrado.");

            // Check if user is already participating in this garden
            var existingParticipation = await _participationRepository.GetActiveParticipationAsync(
                participationDto.UserId, participationDto.GardenId);
            if (existingParticipation != null)
                throw new ArgumentException("Usuário já está participando deste jardim.");

            var participation = _mapper.Map<Participation>(participationDto);
            var createdParticipation = await _participationRepository.CreateParticipationAsync(participation);
            
            var result = await _participationRepository.GetParticipationByIdAsync(createdParticipation.Id);
            return _mapper.Map<ParticipationResponseDTO>(result);
        }

        public async Task<ParticipationResponseDTO?> GetParticipationByIdAsync(int id)
        {
            var participation = await _participationRepository.GetParticipationByIdAsync(id);
            return participation != null ? _mapper.Map<ParticipationResponseDTO>(participation) : null;
        }

        public async Task<List<ParticipationResponseDTO>> GetParticipationsByGardenAsync(int gardenId)
        {
            var participations = await _participationRepository.GetParticipationsByGardenAsync(gardenId);
            return _mapper.Map<List<ParticipationResponseDTO>>(participations);
        }

        public async Task<List<ParticipationResponseDTO>> GetParticipationsByUserAsync(int userId)
        {
            var participations = await _participationRepository.GetParticipationsByUserAsync(userId);
            return _mapper.Map<List<ParticipationResponseDTO>>(participations);
        }

        public async Task<ParticipationResponseDTO> UpdateParticipationRoleAsync(int id, string role)
        {
            var existingParticipation = await _participationRepository.GetParticipationByIdAsync(id);
            if (existingParticipation == null)
                throw new ArgumentException("Participação não encontrada.");

            existingParticipation.Role = role;
            var updatedParticipation = await _participationRepository.UpdateParticipationAsync(existingParticipation);
            
            var result = await _participationRepository.GetParticipationByIdAsync(updatedParticipation.Id);
            return _mapper.Map<ParticipationResponseDTO>(result);
        }

        public async Task<bool> LeaveGardenAsync(int userId, int gardenId)
        {
            var participation = await _participationRepository.GetActiveParticipationAsync(userId, gardenId);
            if (participation == null)
                return false;

            return await _participationRepository.DeleteParticipationAsync(participation.Id);
        }

        public async Task<bool> UserCanParticipateInGardenAsync(int userId, int gardenId)
        {
            // Check if user exists and is active
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || !user.IsActive)
                return false;

            // Check if garden exists and is active
            var garden = await _gardenRepository.GetGardenByIdAsync(gardenId);
            if (garden == null || !garden.IsActive)
                return false;

            // Check if user is not already participating
            return !await _participationRepository.UserParticipatesInGardenAsync(userId, gardenId);
        }
    }
}