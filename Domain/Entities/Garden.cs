using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Domain.Entities
{
    public class Garden
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Area { get; set; } // in square meters
        public string? SoilType { get; set; }
        public bool IsPublic { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public User CreatedByUser { get; set; } = null!;
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<Participation> Participations { get; set; } = new List<Participation>();
        public List<PlantedCrop> PlantedCrops { get; set; } = new List<PlantedCrop>();
        public List<WeatherData> WeatherData { get; set; } = new List<WeatherData>();
    }
}