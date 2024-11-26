using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.BusinessLogic.Validators;
using MovieForum.Data.Entities;
using MovieForum.Data.Entities.Enums;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    private readonly ReviewValidator _validator;

    public ReviewService(IReviewRepository repository, IMapper mapper, ReviewValidator validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return null;
        }
        
        var review = await _repository.GetByIdAsync(id);
        return _mapper.Map<Review>(review);
    }
    
    public async Task<IEnumerable<Review>> GetByMovieIdAsync(Guid movieId)
    {
        var reviews = await _repository.GetByMovieIdAsync(movieId);
        if (!reviews.Any())
        {
            //todo log 
            return new List<Review>(); 
        }
        
        return _mapper.Map<IEnumerable<Review>>(reviews);
    }

    public async Task<IEnumerable<Review>> GetReviewsByMovieIdAndRating(Guid movieId, RatingEnum ratingEnum)
    {
        if (movieId.Equals(Guid.Empty))
        {
            //todo log 
            return new List<Review>();
        }
        
        var reviews = await _repository.GetReviewsByMovieIdAndRating(movieId, ratingEnum);
        if (!reviews.Any())
        {
            //todo log 
            return new List<Review>(); 
        }
        
        return _mapper.Map<IEnumerable<Review>>(reviews);
    }

    public async Task<IEnumerable<Review>> GetByUserIdAndMovieIdAsync(Guid userId, Guid movieId)
    {
        if (movieId.Equals(Guid.Empty) || userId.Equals(Guid.Empty))
        {
            return new List<Review>();
        }
        
        var reviews = await _repository.GetByUserIdAndMovieIdAsync(userId, movieId);
        if (!reviews.Any())
        {
            //todo log 
            return new List<Review>(); 
        }
        
        return _mapper.Map<IEnumerable<Review>>(reviews);
    }    
    
    public async Task<IEnumerable<Movie>> GetByMovieIdAndPublishDateAsync(Guid moveId, DateTime from, DateTime to)
    {
        if (moveId.Equals(Guid.Empty) || from > to)
        {
            return new List<Movie>();
        }
        
        var reviews = await _repository.GetByMovieIdAndPublishDateAsync(moveId, from, to);
        if (!reviews.Any())
        {
            //todo log 
            return new List<Movie>(); 
        }
        
        return _mapper.Map<IEnumerable<Movie>>(reviews);    
    }
    
    public async Task<Guid> AddAsync(Review review)
    {
        var validationResult = await _validator.ValidateAsync(review);
        if (!validationResult.IsValid)
        {
            //todo log
            return Guid.Empty;
        }
        var reviewEntity = _mapper.Map<ReviewEntity>(review);
        var id = await _repository.AddAsync(reviewEntity);
        if (id.Equals(Guid.Empty))
        {
            //todo log
            return Guid.Empty;
        }
        return id;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return false;
        }
        
        var isDeleted = await _repository.DeleteAsync(id);
        return isDeleted;
    }
    
    public async Task<bool> UpdateAsync(Review review)
    {
        var validationResult = await _validator.ValidateAsync(review);
        if (!validationResult.IsValid)
        {
            //todo log
            return false;
        }
        
        var reviewEntity = _mapper.Map<ReviewEntity>(review);
        return await _repository.UpdateAsync(reviewEntity);
    }
}