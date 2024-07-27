using AutoMapper;
using TaskManagerSample.API.ViewModels;

namespace TaskManagerSample.API.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Core.Models.Task, TaskViewModel>()
            .ReverseMap();

        CreateMap<Core.Models.User, RegisterUserViewModel>()
            .ReverseMap();

        CreateMap<Core.Models.User, UserViewModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, src => src.MapFrom(src => src.Role));
    }
}