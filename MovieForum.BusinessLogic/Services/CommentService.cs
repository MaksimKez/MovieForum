using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return null;
        }
        
        var comment = await _commentRepository.GetByIdAsync(id);
        return _mapper.Map<Comment>(comment);
    }

    public async Task<IEnumerable<Comment>> GetByIsPositiveStatusAndReviewIdAsync(bool isPositive, Guid reviewId)
    {
        if (reviewId.Equals(Guid.Empty))
        {
            return new List<Comment>();
        }
        
        var comments = await _commentRepository.GetByIsPositiveStatusAndReviewIdAsync(isPositive, reviewId);
        
        return _mapper.Map<IEnumerable<Comment>>(comments);
    }
    
    public async Task<Guid> AddAsync(Comment comment)
    {
        //todo add validation
        
        var commentEntity = _mapper.Map<CommentEntity>(comment);
        return await _commentRepository.AddAsync(commentEntity);
    }
    
    public async Task<bool> UpdateAsync(Comment comment)
    {
        //todo add validation
        
        var commentEntity = _mapper.Map<CommentEntity>(comment);
        var isUpdated = await _commentRepository.UpdateAsync(commentEntity);
        return isUpdated;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var isDeleted = await _commentRepository.DeleteAsync(id);
        return isDeleted;
    }

    public async Task<IEnumerable<Comment>> GetByDateAndUserIdAsync(Guid userId, DateTime from, DateTime to)
    {
        var isInputValid = from < to && userId != Guid.Empty;
        if (!isInputValid)
        {
            return new List<Comment>();
        }
        
        var comments = await _commentRepository.GetByDateAndUserIdAsync(userId, from, to);
        return _mapper.Map<IEnumerable<Comment>>(comments);
    }

    public async Task<IEnumerable<Comment>> GetByDateAndByReviewIdAsync(Guid reviewId, DateTime from, DateTime to)
    {
        var isInputValid = from < to && reviewId != Guid.Empty;
        if (!isInputValid)
        {
            return new List<Comment>();
        }
        
        var comments = await _commentRepository.GetByDateAndByReviewIdAsync(reviewId, from, to);
        return _mapper.Map<IEnumerable<Comment>>(comments);
    }
}