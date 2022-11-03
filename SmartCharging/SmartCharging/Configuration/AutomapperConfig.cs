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
            CreateMap<Group, GroupViewDto>().ReverseMap();
            CreateMap<ChargeStation, ChargeStationAddDto>().ReverseMap();
            CreateMap<ChargeStation, ChargeStationViewDto>().ReverseMap();
            CreateMap<Connector, ConnectorDto>().ReverseMap();
        }

    }
}
