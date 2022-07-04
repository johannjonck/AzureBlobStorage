using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Assemblers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<User, UserDto>();
            this.CreateMap<UserDto, User>();
        }   
    }
}
