using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Task mappings
            CreateMap<CreateTaskDTO, CommunityPlant.Domain.Entities.Task>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));
            
            CreateMap<CommunityPlant.Domain.Entities.Task, TaskDTO>().ReverseMap();
            CreateMap<CommunityPlant.Domain.Entities.Task, TaskResponseDTO>();

            // Garden mappings
            CreateMap<CreateGardenDTO, Garden>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            
            CreateMap<Garden, GardenDTO>().ReverseMap();
            CreateMap<Garden, GardenResponseDTO>();

            // User mappings
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserResponseDTO>();

            // Plant mappings
            CreateMap<CreatePlantDTO, Plant>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            
            CreateMap<Plant, PlantDTO>().ReverseMap();
            CreateMap<Plant, PlantResponseDTO>();

            // PlantedCrop mappings
            CreateMap<CreatePlantedCropDTO, PlantedCrop>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Planted"));
            
            CreateMap<PlantedCrop, PlantedCropDTO>().ReverseMap();
            CreateMap<PlantedCrop, PlantedCropResponseDTO>();

            // Participation mappings
            CreateMap<CreateParticipationDTO, Participation>()
                .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            
            CreateMap<Participation, ParticipationDTO>().ReverseMap();
            CreateMap<Participation, ParticipationResponseDTO>();

            // WeatherData mappings
            CreateMap<CreateWeatherDataDTO, WeatherData>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            
            CreateMap<WeatherData, WeatherDataDTO>().ReverseMap();
            CreateMap<WeatherData, WeatherDataResponseDTO>();
        }
    }

}
