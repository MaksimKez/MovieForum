using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities;

namespace MovieForum.BusinessLogic.Helpers.Mappers;

public class CommentMapperProfile : Profile
{
    public CommentMapperProfile()
    {
        CreateMap<Comment, CommentEntity>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Review, opt => opt.Ignore());

        CreateMap<CommentEntity, Comment>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId));
    }
}