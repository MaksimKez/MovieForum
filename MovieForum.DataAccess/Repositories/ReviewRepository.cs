using Microsoft.EntityFrameworkCore;
using MovieForum.Data;
using MovieForum.Data.Entities;
using MovieForum.Data.Entities.Enums;
using MovieForum.Data.Interfaces;

namespace MovieForum.DataAccess.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<ReviewEntity?> GetByIdAsync(Guid id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        return review;
    }

    public async Task<IEnumerable<ReviewEntity>> GetByMovieIdAsync(Guid movieId)
    {
        var reviews = await _context.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
        return reviews;
    }

    public async Task<IEnumerable<ReviewEntity>> GetReviewsByMovieIdAndRating(Guid movieId, RatingEnum ratingEnum)
    {
        var reviews = await _context.Reviews.Where(r => r.MovieId == movieId && r.Rating == ratingEnum).ToListAsync();
        return reviews;
    }

    public async Task<IEnumerable<ReviewEntity>> GetByUserIdAndMovieIdAsync(Guid userId, Guid movieId)
    {
        var reviews = await _context.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).ToListAsync();
        return reviews;
    }

    public async Task<IEnumerable<ReviewEntity>> GetByMovieIdAndPublishDateAsync(Guid movieId, DateTime from, DateTime to)
    {
        var reviews = await _context.Reviews.Where(r => r.MovieId == movieId && r.PublishDate >= from && r.PublishDate <= to).ToListAsync();
        return reviews;
    }

    public async Task<Guid> AddAsync(ReviewEntity review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review.Id;
    }

    public async Task<bool> UpdateAsync(ReviewEntity review)
    {
        var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
        if (reviewToUpdate == null)
        {
            return false;
        }
        
        _context.Reviews.Update(review);
        _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        if (reviewToDelete == null)
        {
            return false;
        }
        
        _context.Reviews.Remove(reviewToDelete);
        _context.SaveChangesAsync();
        return true;
    }
}