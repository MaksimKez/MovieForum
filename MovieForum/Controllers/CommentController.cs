using Microsoft.AspNetCore.Mvc;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;

namespace MovieForum.Controllers;

[ApiController]
[Route("api/comments")] // Общий базовый маршрут для контроллера
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{id:guid}", Name = "GetCommentById")]
    public async Task<ActionResult<Comment>> Get(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpGet("filter-by-status-and-review/{isPositive}/{reviewId:guid}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetByIsPositiveStatusAndReviewId(bool isPositive, Guid reviewId)
    {
        var comments = await _commentService.GetByIsPositiveStatusAndReviewIdAsync(isPositive, reviewId);
        if (!comments.Any())
        {
            return NotFound();
        }

        return Ok(comments);
    }

    [HttpGet("filter-by-date-and-review/{reviewId:guid}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetByDateAndByReviewId(Guid reviewId, DateTime from, DateTime to)
    {
        var comments = await _commentService.GetByDateAndByReviewIdAsync(reviewId, from, to);
        if (!comments.Any())
        {
            return NotFound();
        }

        return Ok(comments);
    }

    [HttpGet("filter-by-date-and-user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetByDateAndUserId(Guid userId, DateTime from, DateTime to)
    {
        var comments = await _commentService.GetByDateAndUserIdAsync(userId, from, to);
        if (!comments.Any())
        {
            return NotFound();
        }

        return Ok(comments);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add(Comment comment)
    {
        var id = await _commentService.AddAsync(comment);
        if (id == Guid.Empty)
        {
            return BadRequest("Unable to create the comment.");
        }

        return CreatedAtRoute(
            routeName: "GetCommentById",
            routeValues: new { id },
            value: id
        );
    }

    [HttpPut]
    public async Task<ActionResult> Update(Comment comment)
    {
        var isUpdated = await _commentService.UpdateAsync(comment);
        if (!isUpdated)
        {
            return BadRequest("Unable to update the comment.");
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var isDeleted = await _commentService.DeleteAsync(id);
        if (!isDeleted)
        {
            return BadRequest("Unable to delete the comment.");
        }

        return NoContent();
    }
}
