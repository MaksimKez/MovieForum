using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities;

namespace MovieForum.BusinessLogic.Helpers.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
        
        CreateMap<UserEntity, User>();
    }
}