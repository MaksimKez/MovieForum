using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities;

namespace MovieForum.BusinessLogic.Helpers.Mappers;

public class ReviewMapperProfile : Profile
{
    public ReviewMapperProfile()
    {
        CreateMap<ReviewEntity, Review>();
            
        CreateMap<Review, ReviewEntity>() 
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}