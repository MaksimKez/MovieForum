using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface ICommentService
{
    Task<Comment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Comment>> GetByIsPositiveStatusAndReviewIdAsync(bool isPositive, Guid reviewId);
    Task<Guid> AddAsync(Comment comment);
    Task<bool> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<Comment>> GetByDateAndUserIdAsync(Guid userId, DateTime from, DateTime to);
    Task<IEnumerable<Comment>> GetByDateAndByReviewIdAsync(Guid reviewId, DateTime from, DateTime to);
}
