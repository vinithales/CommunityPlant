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
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public PlantService(IPlantRepository plantRepository, IMapper mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        public async Task<PlantResponseDTO> CreatePlantAsync(CreatePlantDTO plantDto)
        {
            if (string.IsNullOrWhiteSpace(plantDto.Name))
                throw new ArgumentException("Nome da planta é obrigatório.");

            if (plantDto.DaysToHarvest <= 0)
                throw new ArgumentException("Dias para colheita deve ser maior que zero.");

            var plant = _mapper.Map<Plant>(plantDto);
            var createdPlant = await _plantRepository.CreatePlantAsync(plant);
            return _mapper.Map<PlantResponseDTO>(createdPlant);
        }

        public async Task<PlantResponseDTO?> GetPlantByIdAsync(int id)
        {
            var plant = await _plantRepository.GetPlantByIdAsync(id);
            return plant != null ? _mapper.Map<PlantResponseDTO>(plant) : null;
        }

        public async Task<List<PlantResponseDTO>> GetAllPlantsAsync()
        {
            var plants = await _plantRepository.GetAllPlantsAsync();
            return _mapper.Map<List<PlantResponseDTO>>(plants);
        }

        public async Task<List<PlantResponseDTO>> GetPlantsByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Tipo da planta é obrigatório.");

            var plants = await _plantRepository.GetPlantsByTypeAsync(type);
            return _mapper.Map<List<PlantResponseDTO>>(plants);
        }

        public async Task<List<PlantResponseDTO>> SearchPlantsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Termo de busca é obrigatório.");

            var plants = await _plantRepository.SearchPlantsAsync(searchTerm);
            return _mapper.Map<List<PlantResponseDTO>>(plants);
        }

        public async Task<PlantResponseDTO> UpdatePlantAsync(int id, PlantDTO plantDto)
        {
            var existingPlant = await _plantRepository.GetPlantByIdAsync(id);
            if (existingPlant == null)
                throw new ArgumentException("Planta não encontrada.");

            _mapper.Map(plantDto, existingPlant);
            var updatedPlant = await _plantRepository.UpdatePlantAsync(existingPlant);
            return _mapper.Map<PlantResponseDTO>(updatedPlant);
        }

        public async Task<bool> DeletePlantAsync(int id)
        {
            return await _plantRepository.DeletePlantAsync(id);
        }
    }
}