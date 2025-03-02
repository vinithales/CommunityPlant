using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPlant.Domain.Entities
{
    public class Task
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description{get; set;}
        public DateTime DueDate {get; set;}
        public string Status {get; set;}
        public int GardenId {get; set;}
        public Garden Garden {get; set;}
    }
}