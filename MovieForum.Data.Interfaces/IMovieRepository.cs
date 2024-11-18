using MovieForum.Data.Entities;

namespace MovieForum.Data.Interfaces;

public interface IMovieRepository
{
    Task<MovieEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<MovieEntity>> GetAllAsync();
    Task<IEnumerable<MovieEntity>> GetByReleaseDate(DateTime from, DateTime to);
    Task<IEnumerable<MovieEntity>> GetByRating(double from, double to);
    Task<IEnumerable<MovieEntity>> GetTop100Async();
}