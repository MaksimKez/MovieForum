using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieForum.BusinessLogic.auth.Interfaces;

namespace MovieForum.BusinessLogic.auth;

public class JwtProvider : IJwtProvider
{
    public string GenerateJwtToken(string email, Guid userId)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey("superSecretKey@345"u8.ToArray()),
            SecurityAlgorithms.Sha256);
        
        var claims = new[]
        {
            new Claim("Email", email),
            new Claim("UserId", userId.ToString())
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey("superSecretKey@345"u8.ToArray()),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}