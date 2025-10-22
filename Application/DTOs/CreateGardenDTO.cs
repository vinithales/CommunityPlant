using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Application.DTOs
{
    public class CreateGardenDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Area { get; set; }
        public string? SoilType { get; set; }
        public bool IsPublic { get; set; } = true;
        public int CreatedByUserId { get; set; }
    }
}