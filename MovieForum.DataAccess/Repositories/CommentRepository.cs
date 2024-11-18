using Microsoft.EntityFrameworkCore;
using MovieForum.Data;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CommentEntity?> GetByIdAsync(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        return comment ?? null;
    }
    
    private IEnumerable<CommentEntity> FilterByDate(IEnumerable<CommentEntity> commentsToFilter,
        DateTime from, DateTime to)
    {
        commentsToFilter = commentsToFilter.Where(c => c.PublishedAt >= from && c.PublishedAt <= to);
        return commentsToFilter;
    }

    public async Task<IEnumerable<CommentEntity>> GetByDateAsync(Guid userId, DateTime from, DateTime to)
    {
        var comments = await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        
        return FilterByDate(comments, from, to);
    }

    public async Task<IEnumerable<CommentEntity>> GetByReviewIdAsync(Guid reviewId, DateTime from, DateTime to)
    {
        var comments = await _context.Comments.Where(c => c.ReviewId == reviewId).ToListAsync();
        
        return FilterByDate(comments, from, to);
    }
}