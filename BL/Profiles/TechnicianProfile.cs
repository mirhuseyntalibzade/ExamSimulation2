using AutoMapper;
using BL.DTOs.ServiceDTO;
using BL.DTOs.TechnicianDTO;
using CORE.Models;

namespace BL.Profiles
{
    public class TechnicianProfile : Profile
    {
        public TechnicianProfile()
        {
            CreateMap<Technician,AddTechnicianDTO>().ReverseMap();
            CreateMap<Technician,GetTechnicianDTO>().ReverseMap();
            CreateMap<Technician,UpdateTechnicianDTO>().ReverseMap();
            CreateMap<GetTechnicianDTO, UpdateTechnicianDTO>().ReverseMap();
            CreateMap<AddTechnicianDTO, UpdateTechnicianDTO>().ReverseMap();

        }
    }
}
