using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Application.DTOs
{
    public class CreateTaskDTO
    {
        public string Name {get; set;}
        public string Description {get; set;}
        public int GardenId {get; set;}
        public DateTime DueDate{get; set;}
         public string Status { get; } = "Pending";
        
    }
}