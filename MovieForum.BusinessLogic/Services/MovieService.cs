using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }
    
    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty))
        {
            return null;
        }
        
        var movie = await _movieRepository.GetByIdAsync(id);
        return _mapper.Map<Movie>(movie);
    }

    public async Task<IEnumerable<Movie>> GetByTitleAsync(string title)
    {
        if (title.Equals(String.Empty))
        {
            return new List<Movie>();
        }
        
        var movies = await _movieRepository.GetByTitleAsync(title);
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }
    
    public async Task<IEnumerable<Movie>> GetTop100Async()
    {
        var movies = await _movieRepository.GetTop100Async();
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }
    
    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }   
    
    public async Task<IEnumerable<Movie>> GetByRating(double from, double to)
    {
        if (to > from)
        {
            return new List<Movie>();
        }
        
        var movies = await _movieRepository.GetByRating(from, to);
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }
    
    public async Task<IEnumerable<Movie>> GetByReleaseDate(DateTime from, DateTime to)
    {
        if (to < from)
        {
            return new List<Movie>();
        }
        
        var movies = await _movieRepository.GetByReleaseDate(from, to);
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }
    
    public async Task<Guid> AddAsync(Movie movie)
    {
        //todo add validation
        
        var movieEntity = _mapper.Map<MovieEntity>(movie);
        return await _movieRepository.AddAsync(movieEntity);
    }
    
    public async Task<bool> UpdateAsync(Movie movie)
    {
        //todo add validation
        
        var movieEntity = _mapper.Map<MovieEntity>(movie);
        return await _movieRepository.UpdateAsync(movieEntity);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return false;
        }
        
        return await _movieRepository.DeleteAsync(id);
    }
}