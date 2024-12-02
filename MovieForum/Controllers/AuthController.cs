using Microsoft.AspNetCore.Mvc;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Dtos;

namespace MovieForum.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Login([FromBody] UserLogInDto request)
    {
        var token = await _service.LoginAsync(request.Email, request.Password);
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid email or password.");
        }
        
        return Ok(token);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] UserRegistrationDto request)
    {
        var isSuccess = await _service.RegisterAsync(new User
            {
                Email = request.Email,
                PasswordHash = request.Password,
                Username = request.Username,
                FullName = request.FullName
            });
        
        if (!isSuccess)
        {
            return BadRequest("Unable to register the user.");
        }
        
        return Ok("User registered successfully.");
    }
    
}