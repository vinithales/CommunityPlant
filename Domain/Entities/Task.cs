using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int GardenId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Priority { get; set; } = "Medium"; // Low, Medium, High
        
        // Navigation Properties
        public Garden Garden { get; set; } = null!;
        public User? AssignedToUser { get; set; }
        public User? CreatedByUser { get; set; }
        public List<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
    }
}