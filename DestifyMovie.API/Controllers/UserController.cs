using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DestifyMovie.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using DestifyMovie.Services.Interfaces;

namespace DestifyMovie.API.Controllers;

/// <summary>
/// Manages user-related operations, such as login and registration.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Logs in a user and returns their JWT token.
    /// </summary>
    /// <param name="loginModel">The user's login details (username and password).</param>
    /// <returns>The JWT token for the logged-in user.</returns>
    /// <response code="200">Returns the JWT token.</response>
    /// <response code="400">If the login request is invalid.</response>
    /// <response code="401">If the username or password is incorrect.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid login data." });
        }

        try
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user == null) return Unauthorized(new { error = "Invalid username or password." });

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded) return Unauthorized(new { error = "Invalid username or password." });

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.UserName, roles);

            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to log in.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerModel">The user's registration details.</param>
    /// <returns>A success message or an error.</returns>
    /// <response code="200">If the user is registered successfully.</response>
    /// <response code="400">If the registration data is invalid.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid registration data." });
        }

        try
        {
            var user = new User
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { error = "Failed to register user.", details = result.Errors });
            }

            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Failed to register user.", detail = ex.Message });
        }
    }
}
