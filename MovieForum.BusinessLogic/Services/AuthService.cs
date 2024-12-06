using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MovieForum.BusinessLogic.auth;
using MovieForum.BusinessLogic.auth.Interfaces;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.BusinessLogic.Validators;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly UserValidator _validator;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper, IJwtProvider jwtProvider, UserValidator validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _validator = validator;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        if (user.PasswordHash == null)
        {
            //logic for oauth
            return false;
        }
        
        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            //todo log
            return false;
        }
        
        var userEntity = _mapper.Map<UserEntity>(user);
        
        var salt = _passwordHasher.GenerateSalt(16);
        var hashedPassword = _passwordHasher.HashPassword(user.PasswordHash, salt);
        userEntity.PasswordHash = hashedPassword;
        userEntity.PasswordSalt = salt;
        
        
        
        var id = await _userRepository.AddAsync(userEntity);
        return id != Guid.Empty;
    }

    public async Task<TokensModel> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user?.PasswordHash == null || user.PasswordSalt == null)
        {
            // todo log
            return null;
        }

        var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
        if (!isValidPassword)
        {
            // todo log
            return null;
        }

        var accessToken = _jwtProvider.GenerateJwtToken(user.Email, user.Id);

        var refreshToken = _jwtProvider.GenerateJwtToken(user.Email, user.Id, isRefreshToken: true);

        //todo add refresh token to user
        await _userRepository.AddRefreshTokenAsync(user.Id, refreshToken);
        

        return new TokensModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<string?> RefreshAccessTokenAsync(string refreshToken)
    {
        if (!_jwtProvider.ValidateJwtToken(refreshToken))
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        var claims = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken).Claims;
        var userId = new Guid(claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

        // Проверка существования refresh token в БД
        var storedRefreshToken = await _userRepository.GetRefreshTokenAsync(userId);
        if (storedRefreshToken != refreshToken)
        {
            throw new UnauthorizedAccessException("Refresh token mismatch.");
        }

        var accessToken = _jwtProvider.GenerateJwtToken(claims.First(c => c.Type == "Email").Value, userId);

        return accessToken;
    }
}
