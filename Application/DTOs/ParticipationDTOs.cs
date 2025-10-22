using System;

namespace CommunityPlant.Application.DTOs
{
    public class CreateParticipationDTO
    {
        public int UserId { get; set; }
        public int GardenId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    public class ParticipationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GardenId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
    }

    public class ParticipationResponseDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; } = null!;
        public GardenDTO Garden { get; set; } = null!;
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
    }
}