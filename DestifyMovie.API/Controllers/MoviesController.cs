using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DestifyMovie.Data.Entities;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.API.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DestifyMovie.API.Controllers;

/// <summary>
/// Handles movie-related operations such as retrieving, creating, updating, and deleting movies.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;
    private readonly IActorService _actorService;
    private readonly IMapper _mapper;

    public MoviesController(IMovieService service, IActorService actorService, IMapper mapper)
    {
        _service = service;
        _actorService = actorService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all movies.
    /// </summary>
    /// <returns>A list of movies.</returns>
    /// <response code="200">Returns the list of movies.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var movies = await _service.GetAllAsync();
            var movieViewModels = _mapper.Map<List<MovieViewModel>>(movies);
            return Ok(movieViewModels);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to retrieve movies.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves a movie by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The requested movie.</returns>
    /// <response code="200">Returns the movie.</response>
    /// <response code="404">If the movie is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var movie = await _service.GetByIdAsync(id);
            if (movie == null) return NotFound(new { error = "Movie not found." });

            var movieViewModel = _mapper.Map<MovieViewModel>(movie);
            return Ok(movieViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to retrieve movie.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new movie.
    /// </summary>
    /// <param name="input">The details of the movie to create.</param>
    /// <returns>The created movie's details.</returns>
    /// <response code="201">Returns the created movie's details.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(MovieInputView input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data provided." });
        }

        try
        {
            // Verificar se os atores existem no banco
            var existingActors = await _actorService.GetByIdsAsync(input.ActorIds);
            if (existingActors.Count() != input.ActorIds.Count)
            {
                return BadRequest(new { error = "One or more actors do not exist." });
            }

            // Mapear o filme e associar os atores existentes
            var movie = _mapper.Map<Movie>(input);
            movie.Actors = existingActors.ToList();

            await _service.AddAsync(movie);
            var movieViewModel = _mapper.Map<MovieViewModel>(movie);

            return CreatedAtAction(nameof(GetById), new { id = movieViewModel.Id }, movieViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to create movie.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing movie.
    /// </summary>
    /// <param name="id">The ID of the movie to update.</param>
    /// <param name="input">The new details of the movie.</param>
    /// <response code="204">If the update is successful.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, MovieInputView input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data provided." });
        }

        try
        {
            
            var existingMovie = await _service.GetByIdAsync(id);
            if (existingMovie == null)
            {
                return NotFound(new { error = "Movie not found." });
            }
           
            var existingActors = await _actorService.GetByIdsAsync(input.ActorIds);
            if (existingActors.Count() != input.ActorIds.Count)
            {
                return BadRequest(new { error = "One or more actors do not exist in the database." });
            }
            
            existingMovie.Title = input.Title;
            existingMovie.ReleaseYear = input.ReleaseYear;

           
            var actorsToAdd = existingActors
                .Where(a => !existingMovie.Actors.Any(existing => existing.Id == a.Id))
                .ToList();

            var actorsToRemove = existingMovie.Actors
                .Where(existing => !input.ActorIds.Contains(existing.Id))
                .ToList();

            
            foreach (var actor in actorsToAdd)
            {
                existingMovie.Actors.Add(actor);
            }

            foreach (var actor in actorsToRemove)
            {
                existingMovie.Actors.Remove(actor);
            }

           
            await _service.UpdateAsync(existingMovie);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to update movie.", detail = ex.Message });
        }
    }


    /// <summary>
    /// Deletes a movie by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <response code="204">If the deletion is successful.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to delete movie.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Searches movies by title.
    /// </summary>
    /// <param name="title">The partial title to search for.</param>
    /// <returns>A list of movies matching the search criteria.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<MovieViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchByName([FromQuery] string title)
    {
        try
        {
            var movies = await _service.SearchByNameAsync(title);
            var movieViewModels = _mapper.Map<List<MovieViewModel>>(movies);
            return Ok(movieViewModels);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to search movies.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Adds a rating to a movie.
    /// </summary>
    /// <param name="id">The ID of the movie to rate.</param>
    /// <param name="input">The rating details.</param>
    /// <returns>The created rating.</returns>
    /// <response code="200">If the rating is added successfully.</response>
    /// <response code="404">If the movie is not found.</response>
    [HttpPost("{id}/ratings")]
    [ProducesResponseType(typeof(MovieRatingViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRating(int id, [FromBody] MovieRatingInputView input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid rating data.");
        }

        var movie = await _service.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound($"Movie with ID {id} not found.");
        }

        try
        {
            var rating = new MovieRating
            {
                Rating = input.Rating,
                Comment = input.Comment,
                MovieId = id
            };

            await _service.AddRatingAsync(rating);

            // Map the result to a ViewModel
            var ratingViewModel = _mapper.Map<MovieRatingViewModel>(rating);

            return Ok(ratingViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to add rating.", detail = ex.Message });
        }
    }
}
