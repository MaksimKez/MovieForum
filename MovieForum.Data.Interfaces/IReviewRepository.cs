using MovieForum.Data.Entities;
using MovieForum.Data.Entities.Enums;

namespace MovieForum.Data.Interfaces;

public interface IReviewRepository
{
    Task<ReviewEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<ReviewEntity>> GetByMovieIdAsync(Guid movieId);
    Task<IEnumerable<ReviewEntity>> GetReviewsByMovieIdAndRating(Guid movieId, RatingEnum ratingEnum);
    Task<IEnumerable<ReviewEntity>> GetByUserIdAndMovieIdAsync(Guid userId, Guid movieId); 
    Task<IEnumerable<ReviewEntity>> GetByMovieIdAndPublishDateAsync(Guid movieId, DateTime from, DateTime to);
    
    Task<Guid> AddAsync(ReviewEntity review);
    Task<bool> UpdateAsync(ReviewEntity review);
    Task<bool> DeleteAsync(ReviewEntity review);
}