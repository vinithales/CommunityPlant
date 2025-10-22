using System;
using System.Collections.Generic;

namespace CommunityPlant.Domain.Entities
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Vegetable, Fruit, Herb, Flower, etc.
        public string Description { get; set; } = string.Empty;
        public int DaysToHarvest { get; set; }
        public string PlantingSeason { get; set; } = string.Empty; // Spring, Summer, Fall, Winter
        public string WateringFrequency { get; set; } = string.Empty; // Daily, Weekly, etc.
        public string SunlightRequirement { get; set; } = string.Empty; // Full sun, Partial shade, etc.
        public string SoilType { get; set; } = string.Empty;
        public decimal? SpacingDistance { get; set; } // in cm
        public string? CareInstructions { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public List<PlantedCrop> PlantedCrops { get; set; } = new List<PlantedCrop>();
    }
}