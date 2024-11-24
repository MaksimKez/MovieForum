using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities;

namespace MovieForum.BusinessLogic.Helpers.Mappers;

public class MovieMapperProfile : Profile
{
    public MovieMapperProfile()
    {
        CreateMap<Movie, MovieEntity>()
            .ForMember(dest => dest.Reviews, opt => opt.Ignore());

        CreateMap<MovieEntity, Movie>();
    }
}