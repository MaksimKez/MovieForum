using Microsoft.EntityFrameworkCore;
using MovieForum.Data;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.DataAccess.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<MovieEntity?> GetByIdAsync(Guid id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        return movie;
    }

    public async Task<IEnumerable<MovieEntity>> GetByTitleAsync(string title)
    {
        var movies = await _context.Movies.Where(m => m.Title.Contains(title)).ToListAsync();
        return movies;
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        var movies = await _context.Movies.ToListAsync(); 
        return movies;
    }

    public async Task<IEnumerable<MovieEntity>> GetByReleaseDate(DateTime from, DateTime to)
    {
        var movies = await _context.Movies.Where(m => m.ReleaseDate >= from && m.ReleaseDate <= to).ToListAsync();
        return movies;
    }

    public async Task<IEnumerable<MovieEntity>> GetByRating(double from, double to)
    {
        var movies = await _context.Movies.Where(m => m.Rating >= from && m.Rating <= to).ToListAsync();
        return movies;
    }

    public async Task<IEnumerable<MovieEntity>> GetTop100Async()
    {
        var movies = await _context.Movies.OrderByDescending(m => m.Rating).Take(100).ToListAsync();
        return movies;
    }

    public async Task<Guid> AddAsync(MovieEntity movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie.Id;
    }

    public async Task<bool> UpdateAsync(MovieEntity movie)
    {
        var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);
        if (movieToUpdate == null)
        {
            return false;
        }
        
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var movieToDelete = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movieToDelete == null)
        {
            return Task.FromResult(false);
        }
        
        _context.Movies.Remove(movieToDelete);
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}