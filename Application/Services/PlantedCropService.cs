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
    public class PlantedCropService : IPlantedCropService
    {
        private readonly IPlantedCropRepository _plantedCropRepository;
        private readonly IGardenRepository _gardenRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public PlantedCropService(
            IPlantedCropRepository plantedCropRepository, 
            IGardenRepository gardenRepository,
            IPlantRepository plantRepository,
            IMapper mapper)
        {
            _plantedCropRepository = plantedCropRepository;
            _gardenRepository = gardenRepository;
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        public async Task<PlantedCropResponseDTO> CreatePlantedCropAsync(CreatePlantedCropDTO plantedCropDto)
        {
            // Validate garden exists
            if (!await _gardenRepository.ExistsAsync(plantedCropDto.GardenId))
                throw new ArgumentException("Jardim não encontrado.");

            // Validate plant exists
            if (!await _plantRepository.ExistsAsync(plantedCropDto.PlantId))
                throw new ArgumentException("Planta não encontrada.");

            if (plantedCropDto.Quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            var plantedCrop = _mapper.Map<PlantedCrop>(plantedCropDto);
            var createdPlantedCrop = await _plantedCropRepository.CreatePlantedCropAsync(plantedCrop);
            
            var result = await _plantedCropRepository.GetPlantedCropByIdAsync(createdPlantedCrop.Id);
            return _mapper.Map<PlantedCropResponseDTO>(result);
        }

        public async Task<PlantedCropResponseDTO?> GetPlantedCropByIdAsync(int id)
        {
            var plantedCrop = await _plantedCropRepository.GetPlantedCropByIdAsync(id);
            return plantedCrop != null ? _mapper.Map<PlantedCropResponseDTO>(plantedCrop) : null;
        }

        public async Task<List<PlantedCropResponseDTO>> GetPlantedCropsByGardenAsync(int gardenId)
        {
            var plantedCrops = await _plantedCropRepository.GetPlantedCropsByGardenAsync(gardenId);
            return _mapper.Map<List<PlantedCropResponseDTO>>(plantedCrops);
        }

        public async Task<List<PlantedCropResponseDTO>> GetPlantedCropsByUserAsync(int userId)
        {
            var plantedCrops = await _plantedCropRepository.GetPlantedCropsByUserAsync(userId);
            return _mapper.Map<List<PlantedCropResponseDTO>>(plantedCrops);
        }

        public async Task<List<PlantedCropResponseDTO>> GetReadyToHarvestAsync()
        {
            var plantedCrops = await _plantedCropRepository.GetReadyToHarvestAsync();
            return _mapper.Map<List<PlantedCropResponseDTO>>(plantedCrops);
        }

        public async Task<PlantedCropResponseDTO> UpdatePlantedCropStatusAsync(int id, string status)
        {
            var existingPlantedCrop = await _plantedCropRepository.GetPlantedCropByIdAsync(id);
            if (existingPlantedCrop == null)
                throw new ArgumentException("Cultivo não encontrado.");

            existingPlantedCrop.Status = status;
            var updatedPlantedCrop = await _plantedCropRepository.UpdatePlantedCropAsync(existingPlantedCrop);
            
            var result = await _plantedCropRepository.GetPlantedCropByIdAsync(updatedPlantedCrop.Id);
            return _mapper.Map<PlantedCropResponseDTO>(result);
        }

        public async Task<PlantedCropResponseDTO> HarvestCropAsync(int id)
        {
            var existingPlantedCrop = await _plantedCropRepository.GetPlantedCropByIdAsync(id);
            if (existingPlantedCrop == null)
                throw new ArgumentException("Cultivo não encontrado.");

            existingPlantedCrop.Status = "Harvested";
            existingPlantedCrop.ActualHarvestDate = DateTime.UtcNow;
            
            var updatedPlantedCrop = await _plantedCropRepository.UpdatePlantedCropAsync(existingPlantedCrop);
            
            var result = await _plantedCropRepository.GetPlantedCropByIdAsync(updatedPlantedCrop.Id);
            return _mapper.Map<PlantedCropResponseDTO>(result);
        }

        public async Task<bool> DeletePlantedCropAsync(int id)
        {
            return await _plantedCropRepository.DeletePlantedCropAsync(id);
        }
    }
}