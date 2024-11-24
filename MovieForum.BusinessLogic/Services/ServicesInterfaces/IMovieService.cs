using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface IMovieService
{
    Task<Movie?> GetByIdAsync(Guid id);
    Task<IEnumerable<Movie>> GetByTitleAsync(string title);
    Task<IEnumerable<Movie>> GetTop100Async();
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<IEnumerable<Movie>> GetByRating(double from, double to);
    Task<IEnumerable<Movie>> GetByReleaseDate(DateTime from, DateTime to);
    Task<Guid> AddAsync(Movie movie);
    Task<bool> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(Guid id);
}
