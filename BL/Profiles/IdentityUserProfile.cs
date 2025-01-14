using AutoMapper;
using BL.DTOs.AuthDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<LoginDTO, IdentityUser>().ReverseMap();
            CreateMap<RegisterDTO, IdentityUser>().ReverseMap();
        }
    }
}
