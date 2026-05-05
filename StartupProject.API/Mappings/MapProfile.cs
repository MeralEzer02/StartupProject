using AutoMapper;
using StartupProject.Data;
using StartupProject.Data.DTOs;

namespace StartupProject.API.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserCreateDto, User>();
        }
    }
}