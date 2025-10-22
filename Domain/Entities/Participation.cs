using System;

namespace CommunityPlant.Domain.Entities
{
    public class Participation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GardenId { get; set; }
        public string Role { get; set; } = string.Empty; // Admin, Volunteer, Observer
        public DateTime JoinedDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Garden Garden { get; set; } = null!;
    }
}