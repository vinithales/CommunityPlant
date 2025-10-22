using System;

namespace CommunityPlant.Domain.Entities
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; } = string.Empty; // Created, Updated, Completed, Assigned, etc.
        public string? PreviousStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Task Task { get; set; } = null!;
        public User? User { get; set; }
    }
}