using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityPlant.Infrastructure.Repositories
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly AppDbContext _context;

        public WeatherDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WeatherData> CreateWeatherDataAsync(WeatherData weatherData)
        {
            _context.WeatherData.Add(weatherData);
            await _context.SaveChangesAsync();
            return weatherData;
        }

        public async Task<WeatherData?> GetWeatherDataByIdAsync(int id)
        {
            return await _context.WeatherData
                .Include(wd => wd.Garden)
                .FirstOrDefaultAsync(wd => wd.Id == id);
        }

        public async Task<List<WeatherData>> GetWeatherDataByGardenAsync(int gardenId)
        {
            return await _context.WeatherData
                .Where(wd => wd.GardenId == gardenId)
                .OrderByDescending(wd => wd.Date)
                .ToListAsync();
        }

        public async Task<List<WeatherData>> GetWeatherDataByDateRangeAsync(int gardenId, DateTime startDate, DateTime endDate)
        {
            return await _context.WeatherData
                .Where(wd => wd.GardenId == gardenId && 
                           wd.Date >= startDate && 
                           wd.Date <= endDate)
                .OrderBy(wd => wd.Date)
                .ToListAsync();
        }

        public async Task<WeatherData?> GetLatestWeatherDataAsync(int gardenId)
        {
            return await _context.WeatherData
                .Where(wd => wd.GardenId == gardenId)
                .OrderByDescending(wd => wd.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<WeatherData> UpdateWeatherDataAsync(WeatherData weatherData)
        {
            _context.WeatherData.Update(weatherData);
            await _context.SaveChangesAsync();
            return weatherData;
        }

        public async Task<bool> DeleteWeatherDataAsync(int id)
        {
            var weatherData = await _context.WeatherData.FindAsync(id);
            if (weatherData == null) return false;

            _context.WeatherData.Remove(weatherData);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}