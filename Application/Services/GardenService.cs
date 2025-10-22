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
    public class GardenService : IGardenService
    {
        private readonly IGardenRepository _gardenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GardenService(IGardenRepository gardenRepository, IUserRepository userRepository, IMapper mapper)
        {
            _gardenRepository = gardenRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GardenResponseDTO> CreateGardenAsync(CreateGardenDTO gardenDto)
        {
            if (string.IsNullOrWhiteSpace(gardenDto.Name))
                throw new ArgumentException("Nome do jardim é obrigatório.");

            if (string.IsNullOrWhiteSpace(gardenDto.Location))
                throw new ArgumentException("Localização do jardim é obrigatória.");

            // Verify if the user exists
            var user = await _userRepository.GetUserByIdAsync(gardenDto.CreatedByUserId);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado.");

            var garden = _mapper.Map<Garden>(gardenDto);
            var createdGarden = await _gardenRepository.CreateGardenAsync(garden);
            
            // Get the garden with all relationships
            var gardenWithRelations = await _gardenRepository.GetGardenByIdAsync(createdGarden.Id);
            return _mapper.Map<GardenResponseDTO>(gardenWithRelations);
        }

        public async Task<GardenResponseDTO?> GetGardenByIdAsync(int id)
        {
            var garden = await _gardenRepository.GetGardenByIdAsync(id);
            return garden != null ? _mapper.Map<GardenResponseDTO>(garden) : null;
        }

        public async Task<List<GardenResponseDTO>> GetAllGardensAsync()
        {
            var gardens = await _gardenRepository.GetAllGardensAsync();
            return _mapper.Map<List<GardenResponseDTO>>(gardens);
        }

        public async Task<List<GardenResponseDTO>> GetPublicGardensAsync()
        {
            var gardens = await _gardenRepository.GetPublicGardensAsync();
            return _mapper.Map<List<GardenResponseDTO>>(gardens);
        }

        public async Task<List<GardenResponseDTO>> GetGardensByUserAsync(int userId)
        {
            var gardens = await _gardenRepository.GetGardensByUserAsync(userId);
            return _mapper.Map<List<GardenResponseDTO>>(gardens);
        }

        public async Task<GardenResponseDTO> UpdateGardenAsync(int id, GardenDTO gardenDto)
        {
            var existingGarden = await _gardenRepository.GetGardenByIdAsync(id);
            if (existingGarden == null)
                throw new ArgumentException("Jardim não encontrado.");

            _mapper.Map(gardenDto, existingGarden);
            var updatedGarden = await _gardenRepository.UpdateGardenAsync(existingGarden);
            
            // Get the garden with all relationships
            var gardenWithRelations = await _gardenRepository.GetGardenByIdAsync(updatedGarden.Id);
            return _mapper.Map<GardenResponseDTO>(gardenWithRelations);
        }

        public async Task<bool> DeleteGardenAsync(int id)
        {
            return await _gardenRepository.DeleteGardenAsync(id);
        }
    }
}