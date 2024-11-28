using AutoMapper;
using MovieForum.BusinessLogic.auth.Interfaces;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.BusinessLogic.Validators;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; 
    private readonly IMapper _mapper;
    private readonly UserValidator _validator;
    private readonly IPasswordHasher _passwordHasher; 
    
    public UserService(IUserRepository userRepository, IMapper mapper, UserValidator validator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return null;
        }
        
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<User>(user);
    } 
    
    public async Task<IEnumerable<User>> GetByUsernameAsync(string username)
    {
        if (username.Equals(String.Empty))
        {
            return new List<User>();
        }
        var users = await _userRepository.GetByUsernameAsync(username);
        return _mapper.Map<IEnumerable<User>>(users); 
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        if (email.Equals(String.Empty))
        {
            return null;
        }
        var user = await _userRepository.GetByEmailAsync(email);
        return _mapper.Map<User>(user); 
    }

    public async Task<Guid> AddAsync(User user)
    {
        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            //todo log
            return Guid.Empty; 
        }
        var userEntity = _mapper.Map<UserEntity>(user);
        var salt = _passwordHasher.GenerateSalt(16);
        if (user.PasswordHash == null)
        {
            // logic for google auth
            return Guid.Empty;
        }

        var hashedPassword = _passwordHasher.HashPassword(user.PasswordHash, salt);
        
        userEntity.PasswordHash = hashedPassword;
        userEntity.PasswordSalt = salt;
        var id = await _userRepository.AddAsync(userEntity);
        if (id.Equals(Guid.Empty))
        {
            //todo log
            return Guid.Empty; 
        }
        
        return id;
    }

    public async Task<bool> UpdateAsync(User user)
    { 
        var validationResult = await _validator.ValidateAsync(user);   
        if (!validationResult.IsValid)
        {
            //todo log
            return false;
        }
        var userEntity = _mapper.Map<UserEntity>(user);
        return await _userRepository.UpdateAsync(userEntity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return false; 
        }
        var isDeleted = await _userRepository.DeleteAsync(id);
        return isDeleted;
    }
}