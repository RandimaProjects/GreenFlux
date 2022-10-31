using AutoMapper;
using SmartCharging.Domain.Models;
using SmartCharging.Dto;

namespace SmartCharging.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Group, GroupAddDto>().ReverseMap();
        }

    }
}
