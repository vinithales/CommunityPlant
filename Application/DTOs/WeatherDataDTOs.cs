using System;

namespace CommunityPlant.Application.DTOs
{
    public class CreateWeatherDataDTO
    {
        public int GardenId { get; set; }
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Precipitation { get; set; }
        public string? WindDirection { get; set; }
        public decimal? WindSpeed { get; set; }
        public string? Description { get; set; }
    }

    public class WeatherDataDTO
    {
        public int Id { get; set; }
        public int GardenId { get; set; }
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Precipitation { get; set; }
        public string? WindDirection { get; set; }
        public decimal? WindSpeed { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class WeatherDataResponseDTO
    {
        public int Id { get; set; }
        public GardenDTO Garden { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Precipitation { get; set; }
        public string? WindDirection { get; set; }
        public decimal? WindSpeed { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}