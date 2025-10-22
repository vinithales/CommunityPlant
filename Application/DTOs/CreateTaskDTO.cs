using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Application.DTOs
{
    public class CreateTaskDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int GardenId { get; set; }
        public DateTime DueDate { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? CreatedByUserId { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; } = "Pending";
    }
}