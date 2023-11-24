using AutoMapper;
using Identity.Data.DTOs;
using Identity.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Data.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
