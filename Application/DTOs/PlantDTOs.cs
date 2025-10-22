using System;

namespace CommunityPlant.Application.DTOs
{
    public class CreatePlantDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DaysToHarvest { get; set; }
        public string PlantingSeason { get; set; } = string.Empty;
        public string WateringFrequency { get; set; } = string.Empty;
        public string SunlightRequirement { get; set; } = string.Empty;
        public string SoilType { get; set; } = string.Empty;
        public decimal? SpacingDistance { get; set; }
        public string? CareInstructions { get; set; }
    }

    public class PlantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DaysToHarvest { get; set; }
        public string PlantingSeason { get; set; } = string.Empty;
        public string WateringFrequency { get; set; } = string.Empty;
        public string SunlightRequirement { get; set; } = string.Empty;
        public string SoilType { get; set; } = string.Empty;
        public decimal? SpacingDistance { get; set; }
        public string? CareInstructions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PlantResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DaysToHarvest { get; set; }
        public string PlantingSeason { get; set; } = string.Empty;
        public string WateringFrequency { get; set; } = string.Empty;
        public string SunlightRequirement { get; set; } = string.Empty;
        public string SoilType { get; set; } = string.Empty;
        public decimal? SpacingDistance { get; set; }
        public string? CareInstructions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}