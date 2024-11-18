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
}