using AutoMapper;
using BL.DTOs.ServiceDTO;
using CORE.Models;

namespace BL.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, AddServiceDTO>().ReverseMap();
            CreateMap<Service, GetServiceDTO>().ReverseMap();
            CreateMap<Service, UpdateServiceDTO>().ReverseMap();
            CreateMap<GetServiceDTO, UpdateServiceDTO>().ReverseMap();
            CreateMap<AddServiceDTO, UpdateServiceDTO>().ReverseMap();
        }
    }
}
