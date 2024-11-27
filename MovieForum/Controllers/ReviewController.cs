using Microsoft.AspNetCore.Mvc;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Data.Entities.Enums;

namespace MovieForum.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{id:guid}", Name = "GetReviewById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Review>> Get(Guid id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        return Ok(review);
    }

    [HttpGet("movie/{movieId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Review>>> GetByMovieId(Guid movieId)
    {
        var reviews = await _reviewService.GetByMovieIdAsync(movieId);
        if (!reviews.Any())
        {
            return NotFound();
        }

        return Ok(reviews);
    }

    [HttpGet("movie/{movieId:guid}/rating/{ratingEnum}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Review>>> GetByMovieIdAndRating(Guid movieId, RatingEnum ratingEnum)
    {
        var reviews = await _reviewService.GetReviewsByMovieIdAndRating(movieId, ratingEnum);
        if (!reviews.Any())
        {
            return NotFound();
        }

        return Ok(reviews);
    }

    [HttpGet("user/{userId:guid}/movie/{movieId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Review>>> GetByUserIdAndMovieId(Guid userId, Guid movieId)
    {
        var reviews = await _reviewService.GetByUserIdAndMovieIdAsync(userId, movieId);
        if (!reviews.Any())
        {
            return NotFound();
        }

        return Ok(reviews);
    }

    [HttpGet("movie/{movieId:guid}/publish-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Review>>> GetByMovieIdAndPublishDate(Guid movieId, DateTime from, DateTime to)
    {
        var reviews = await _reviewService.GetByMovieIdAndPublishDateAsync(movieId, from, to);
        if (!reviews.Any())
        {
            return NotFound();
        }

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add(Review review)
    {
        var id = await _reviewService.AddAsync(review);
        if (id == Guid.Empty)
        {
            return BadRequest("Unable to create the review.");
        }

        return CreatedAtRoute(
            routeName: "GetReviewById",
            routeValues: new { id },
            value: id
        );
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(Review review)
    {
        var isUpdated = await _reviewService.UpdateAsync(review);
        if (!isUpdated)
        {
            return BadRequest("Unable to update the review.");
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var isDeleted = await _reviewService.DeleteAsync(id);
        if (!isDeleted)
        {
            return BadRequest("Unable to delete the review.");
        }

        return NoContent();
    }
}
