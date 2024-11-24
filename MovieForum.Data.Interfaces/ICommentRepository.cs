using MovieForum.Data.Entities;

namespace MovieForum.Data.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<CommentEntity>> GetByDateAndByReviewIdAsync(Guid reviewId, DateTime from, DateTime to);
    Task<IEnumerable<CommentEntity>> GetByDateAndUserIdAsync(Guid userId, DateTime from, DateTime to); 
    Task<CommentEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<CommentEntity>> GetByIsPositiveStatusAndReviewIdAsync(bool isPositive, Guid reviewId);
    
    
    Task<Guid> AddAsync(CommentEntity comment);   
    Task<bool> UpdateAsync(CommentEntity comment);
    Task<bool> DeleteAsync(Guid id);
    
}