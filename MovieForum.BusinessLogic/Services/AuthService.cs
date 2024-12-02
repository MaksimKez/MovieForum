using AutoMapper;
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

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
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
    
    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user?.PasswordHash == null || user.PasswordSalt == null)
        {
            //todo log
            return null;
        }
        //var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
        var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);

        if (!isValidPassword)
        {
            //todo log
            return null;
        }
        
        var token = _jwtProvider.GenerateJwtToken(user.Email, user.Id);
        return token;
    }
}