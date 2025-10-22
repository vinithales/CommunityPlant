using System;

namespace CommunityPlant.Application.DTOs
{
    public class CreatePlantedCropDTO
    {
        public int GardenId { get; set; }
        public int PlantId { get; set; }
        public int? PlantedByUserId { get; set; }
        public DateTime PlantedDate { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
    }

    public class PlantedCropDTO
    {
        public int Id { get; set; }
        public int GardenId { get; set; }
        public int PlantId { get; set; }
        public int? PlantedByUserId { get; set; }
        public DateTime PlantedDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PlantedCropResponseDTO
    {
        public int Id { get; set; }
        public GardenDTO Garden { get; set; } = null!;
        public PlantDTO Plant { get; set; } = null!;
        public UserDTO? PlantedByUser { get; set; }
        public DateTime PlantedDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}