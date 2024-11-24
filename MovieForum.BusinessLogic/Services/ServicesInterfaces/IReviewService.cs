using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities.Enums;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface IReviewService
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetByMovieIdAsync(Guid movieId);
    Task<IEnumerable<Review>> GetReviewsByMovieIdAndRating(Guid movieId, RatingEnum ratingEnum);
    Task<IEnumerable<Review>> GetByUserIdAndMovieIdAsync(Guid userId, Guid movieId);
    Task<IEnumerable<Movie>> GetByMovieIdAndPublishDateAsync(Guid movieId, DateTime from, DateTime to);
    Task<Guid> AddAsync(Review review);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Review review);
}