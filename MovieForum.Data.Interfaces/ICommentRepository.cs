using MovieForum.Data.Entities;

namespace MovieForum.Data.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<CommentEntity>> GetByReviewIdAsync(Guid reviewId, DateTime from, DateTime to);
    public Task<IEnumerable<CommentEntity>> GetByDateAsync(Guid userId, DateTime from, DateTime to);
    public Task<CommentEntity?> GetByIdAsync(Guid id);
}