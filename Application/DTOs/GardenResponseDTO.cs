using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Application.DTOs
{
    public class GardenResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Area { get; set; }
        public string? SoilType { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public UserDTO CreatedByUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<TaskResponseDTO> Tasks { get; set; } = new List<TaskResponseDTO>();
        public List<ParticipationResponseDTO> Participations { get; set; } = new List<ParticipationResponseDTO>();
    }
}