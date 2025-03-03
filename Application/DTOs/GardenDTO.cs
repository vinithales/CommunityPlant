using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Application.DTOs
{
    public class GardenDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Location {get; set;}
        public DateTime CreatedAt {get; set;}
        public List<Task> Tasks {get; set;}
    }
}