using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DestifyMovie.Data.Entities;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.API.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DestifyMovie.API.Controllers;

/// <summary>
/// Handles actor-related operations, such as retrieving, creating, updating, and deleting actors.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly IActorService _service;
    private readonly IMapper _mapper;

    public ActorsController(IActorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all actors.
    /// </summary>
    /// <returns>A list of all actors.</returns>
    /// <response code="200">Returns the list of actors.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ActorViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var actors = await _service.GetAllAsync();
            var actorViewModels = _mapper.Map<List<ActorViewModel>>(actors);
            return Ok(actorViewModels);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to retrieve actors.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves an actor by their ID.
    /// </summary>
    /// <param name="id">The ID of the actor.</param>
    /// <returns>The requested actor.</returns>
    /// <response code="200">Returns the actor.</response>
    /// <response code="404">If the actor is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return NotFound(new { error = "Actor not found." });

            var actorViewModel = _mapper.Map<ActorViewModel>(actor);
            return Ok(actorViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to retrieve actor.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new actor.
    /// </summary>
    /// <param name="input">The details of the actor to create.</param>
    /// <returns>The created actor's details (ID and Name).</returns>
    /// <response code="201">Returns the created actor's details.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(ActorInputView input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data provided." });
        }

        try
        {
            var actor = _mapper.Map<Actor>(input);
            await _service.AddAsync(actor);

            var actorViewModel = _mapper.Map<ActorViewModel>(actor);
            return CreatedAtAction(nameof(GetById), new { id = actorViewModel.Id }, actorViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to create actor.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing actor.
    /// </summary>
    /// <param name="id">The ID of the actor to update.</param>
    /// <param name="input">The new details of the actor.</param>
    /// <response code="204">If the update is successful.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, ActorInputView input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data provided." });
        }

        try
        {
            var actor = _mapper.Map<Actor>(input);
            actor.Id = id;

            await _service.UpdateAsync(actor);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to update actor.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an actor by their ID.
    /// </summary>
    /// <param name="id">The ID of the actor to delete.</param>
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
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to delete actor.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Searches actors by name.
    /// </summary>
    /// <param name="name">The partial name to search for.</param>
    /// <returns>A list of actors matching the search criteria.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ActorViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchByName([FromQuery] string name)
    {
        try
        {
            var actors = await _service.SearchByNameAsync(name);
            var actorViewModels = _mapper.Map<List<ActorViewModel>>(actors);
            return Ok(actorViewModels);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to search actors.", detail = ex.Message });
        }
    }
}
