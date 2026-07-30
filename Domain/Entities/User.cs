using System;
using System.Collections.Generic;
using CommunityPlant.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CommunityPlant.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public EnumTypeUser TypeUser { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public List<Participation> Participations { get; set; } = new List<Participation>();
        public List<Task> AssignedTasks { get; set; } = new List<Task>();

    }
}
