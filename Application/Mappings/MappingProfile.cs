using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;

namespace CommunityPlant.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateTaskDTO, Task>();
            CreateMap<Task, TaskResponseDTO>()
                .ForMember(dest => dest.GardenName, opt => opt.MapFrom(src => src.Garden.Name));
        }

        CreateMap<Garden, GardenResponseDTO>().ReverseMap();
        CreateMap<User, UserResponseDTO>().ReverseMap();
        CreateMap<TaskHistory, TaskHistoryResponseDTO>().ReverseMap();
        CreateMap<WeatherData, WeatherDataResponseDTO>().ReverseMap();
    }

}
}