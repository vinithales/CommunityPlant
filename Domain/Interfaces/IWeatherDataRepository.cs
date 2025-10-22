using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IWeatherDataRepository
    {
        Task<WeatherData> CreateWeatherDataAsync(WeatherData weatherData);
        Task<WeatherData?> GetWeatherDataByIdAsync(int id);
        Task<List<WeatherData>> GetWeatherDataByGardenAsync(int gardenId);
        Task<List<WeatherData>> GetWeatherDataByDateRangeAsync(int gardenId, DateTime startDate, DateTime endDate);
        Task<WeatherData?> GetLatestWeatherDataAsync(int gardenId);
        Task<WeatherData> UpdateWeatherDataAsync(WeatherData weatherData);
        Task<bool> DeleteWeatherDataAsync(int id);
    }
}