using System;

namespace CommunityPlant.Domain.Entities
{
    public class PlantedCrop
    {
        public int Id { get; set; }
        public int GardenId { get; set; }
        public int PlantId { get; set; }
        public int? PlantedByUserId { get; set; }
        public DateTime PlantedDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty; // Planted, Growing, Ready to Harvest, Harvested, Dead
        public string? Location { get; set; } // Specific location within the garden
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Garden Garden { get; set; } = null!;
        public Plant Plant { get; set; } = null!;
        public User? PlantedByUser { get; set; }
    }
}