using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IWeatherDataService
    {
        Task<WeatherDataResponseDTO> CreateWeatherDataAsync(CreateWeatherDataDTO weatherDataDto);
        Task<WeatherDataResponseDTO?> GetWeatherDataByIdAsync(int id);
        Task<List<WeatherDataResponseDTO>> GetWeatherDataByGardenAsync(int gardenId);
        Task<List<WeatherDataResponseDTO>> GetWeatherDataByDateRangeAsync(int gardenId, DateTime startDate, DateTime endDate);
        Task<WeatherDataResponseDTO?> GetLatestWeatherDataAsync(int gardenId);
        Task<WeatherDataResponseDTO> UpdateWeatherDataAsync(int id, WeatherDataDTO weatherDataDto);
        Task<bool> DeleteWeatherDataAsync(int id);
    }
}