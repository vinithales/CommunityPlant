using System;

namespace CommunityPlant.Domain.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public int GardenId { get; set; }
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; } // in Celsius
        public decimal Humidity { get; set; } // percentage
        public decimal Precipitation { get; set; } // in mm
        public string? WindDirection { get; set; }
        public decimal? WindSpeed { get; set; } // km/h
        public string? Description { get; set; } // Clear, Cloudy, Rainy, etc.
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Garden Garden { get; set; } = null!;
    }
}