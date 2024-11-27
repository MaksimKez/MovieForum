using Microsoft.AspNetCore.Mvc;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;

namespace MovieForum.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("{id:guid}", Name = "GetMovieById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Movie>> Get(Guid id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpGet("search-by-title/{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetByTitle(string title)
    {
        var movies = await _movieService.GetByTitleAsync(title);
        if (!movies.Any())
        {
            return NotFound();
        }

        return Ok(movies);
    }

    [HttpGet("top100")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetTop100()
    {
        var movies = await _movieService.GetTop100Async();
        return Ok(movies);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = await _movieService.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("filter-by-rating")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetByRating(double from, double to)
    {
        var movies = await _movieService.GetByRating(from, to);
        if (!movies.Any())
        {
            return NotFound();
        }

        return Ok(movies);
    }

    [HttpGet("filter-by-release-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetByReleaseDate(DateTime from, DateTime to)
    {
        var movies = await _movieService.GetByReleaseDate(from, to);
        if (!movies.Any())
        {
            return NotFound();
        }

        return Ok(movies);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add(Movie movie)
    {
        var id = await _movieService.AddAsync(movie);
        if (id == Guid.Empty)
        {
            return BadRequest("Unable to create the movie.");
        }

        return CreatedAtRoute(
            routeName: "GetMovieById",
            routeValues: new { id },
            value: id
        );
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(Movie movie)
    {
        var isUpdated = await _movieService.UpdateAsync(movie);
        if (!isUpdated)
        {
            return BadRequest("Unable to update the movie.");
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var isDeleted = await _movieService.DeleteAsync(id);
        if (!isDeleted)
        {
            return BadRequest("Unable to delete the movie.");
        }

        return NoContent();
    }
}
