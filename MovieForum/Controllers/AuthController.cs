using Microsoft.AspNetCore.Mvc;
using MovieForum.BusinessLogic.auth;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Dtos;

namespace MovieForum.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    // Метод для логина
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TokensModel>> Login([FromBody] UserLogInDto request)
    {
        var tokens = await _service.LoginAsync(request.Email, request.Password);

        if (tokens == null)
        {
            return BadRequest("Invalid email or password.");
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, 
            SameSite = SameSiteMode.None, 
            Expires = DateTime.UtcNow.AddMinutes(15)
        };

        if (tokens.RefreshToken == null || tokens.AccessToken == null)
        {
            // todo log
            return new TokensModel();
        }
        
        Response.Cookies.Append("AccessToken", tokens.AccessToken, cookieOptions);

        cookieOptions.Expires = DateTime.UtcNow.AddDays(7);
        Response.Cookies.Append("RefreshToken", tokens.RefreshToken, cookieOptions);

        return Ok(new { Message = "Login successful", Tokens = tokens });
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] UserRegistrationDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
        {
            return BadRequest("All fields are required.");
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = request.Password,
            Username = request.Username,
            FullName = request.FullName
        };

        var isSuccess = await _service.RegisterAsync(user);

        if (!isSuccess)
        {
            return BadRequest("Unable to register the user.");
        }

        return Ok("User registered successfully.");
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TokensModel>> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is missing.");
        }

        // Генерируем новый токен через сервис
        var token = await _service.RefreshAccessTokenAsync(refreshToken);

        if (token == null)
        {
            return BadRequest("Invalid refresh token.");
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(15)
        };

        Response.Cookies.Append("AccessToken", token, cookieOptions);
        cookieOptions.Expires = DateTime.UtcNow.AddDays(7);

        return Ok(new { Message = "Tokens refreshed successfully", Token = token });
    }
}
