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
    public class WeatherDataService : IWeatherDataService
    {
        private readonly IWeatherDataRepository _weatherDataRepository;
        private readonly IGardenRepository _gardenRepository;
        private readonly IMapper _mapper;

        public WeatherDataService(
            IWeatherDataRepository weatherDataRepository,
            IGardenRepository gardenRepository,
            IMapper mapper)
        {
            _weatherDataRepository = weatherDataRepository;
            _gardenRepository = gardenRepository;
            _mapper = mapper;
        }

        public async Task<WeatherDataResponseDTO> CreateWeatherDataAsync(CreateWeatherDataDTO weatherDataDto)
        {
            // Validate garden exists
            if (!await _gardenRepository.ExistsAsync(weatherDataDto.GardenId))
                throw new ArgumentException("Jardim não encontrado.");

            if (weatherDataDto.Temperature < -50 || weatherDataDto.Temperature > 60)
                throw new ArgumentException("Temperatura deve estar entre -50°C e 60°C.");

            if (weatherDataDto.Humidity < 0 || weatherDataDto.Humidity > 100)
                throw new ArgumentException("Umidade deve estar entre 0% e 100%.");

            if (weatherDataDto.Precipitation < 0)
                throw new ArgumentException("Precipitação não pode ser negativa.");

            var weatherData = _mapper.Map<WeatherData>(weatherDataDto);
            var createdWeatherData = await _weatherDataRepository.CreateWeatherDataAsync(weatherData);
            
            var result = await _weatherDataRepository.GetWeatherDataByIdAsync(createdWeatherData.Id);
            return _mapper.Map<WeatherDataResponseDTO>(result);
        }

        public async Task<WeatherDataResponseDTO?> GetWeatherDataByIdAsync(int id)
        {
            var weatherData = await _weatherDataRepository.GetWeatherDataByIdAsync(id);
            return weatherData != null ? _mapper.Map<WeatherDataResponseDTO>(weatherData) : null;
        }

        public async Task<List<WeatherDataResponseDTO>> GetWeatherDataByGardenAsync(int gardenId)
        {
            var weatherDataList = await _weatherDataRepository.GetWeatherDataByGardenAsync(gardenId);
            return _mapper.Map<List<WeatherDataResponseDTO>>(weatherDataList);
        }

        public async Task<List<WeatherDataResponseDTO>> GetWeatherDataByDateRangeAsync(int gardenId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Data inicial deve ser anterior à data final.");

            var weatherDataList = await _weatherDataRepository.GetWeatherDataByDateRangeAsync(gardenId, startDate, endDate);
            return _mapper.Map<List<WeatherDataResponseDTO>>(weatherDataList);
        }

        public async Task<WeatherDataResponseDTO?> GetLatestWeatherDataAsync(int gardenId)
        {
            var weatherData = await _weatherDataRepository.GetLatestWeatherDataAsync(gardenId);
            return weatherData != null ? _mapper.Map<WeatherDataResponseDTO>(weatherData) : null;
        }

        public async Task<WeatherDataResponseDTO> UpdateWeatherDataAsync(int id, WeatherDataDTO weatherDataDto)
        {
            var existingWeatherData = await _weatherDataRepository.GetWeatherDataByIdAsync(id);
            if (existingWeatherData == null)
                throw new ArgumentException("Dados meteorológicos não encontrados.");

            _mapper.Map(weatherDataDto, existingWeatherData);
            var updatedWeatherData = await _weatherDataRepository.UpdateWeatherDataAsync(existingWeatherData);
            
            var result = await _weatherDataRepository.GetWeatherDataByIdAsync(updatedWeatherData.Id);
            return _mapper.Map<WeatherDataResponseDTO>(result);
        }

        public async Task<bool> DeleteWeatherDataAsync(int id)
        {
            return await _weatherDataRepository.DeleteWeatherDataAsync(id);
        }
    }
}