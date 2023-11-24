using AutoMapper;
using Identity.Data.DTOs;
using Identity.Data.Entities;

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
