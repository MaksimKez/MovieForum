using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieForum.BusinessLogic.auth.Interfaces;

namespace MovieForum.BusinessLogic.auth;

public class JwtProvider : IJwtProvider
{
    private readonly string _secretKey = "superSecretKey@345superSecretKey@345";
    private readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(7);

    public string GenerateJwtToken(string email, Guid userId, bool isRefreshToken = false)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim("Email", email),
            new Claim("UserId", userId.ToString())
        };

        var expiration = isRefreshToken
            ? DateTime.UtcNow.Add(_refreshTokenLifetime)
            : DateTime.UtcNow.Add(_accessTokenLifetime);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }


    public bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
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

