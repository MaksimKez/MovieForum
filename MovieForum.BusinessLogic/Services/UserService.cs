using AutoMapper;
using MovieForum.BusinessLogic.Models;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; 
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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
        //todo validation
        var userEntity = _mapper.Map<UserEntity>(user);
        var id = await _userRepository.AddAsync(userEntity);
        if (id.Equals(Guid.Empty))
        {
            //todo log
            return Guid.Empty; 
        }
        
        return id;
    }

    public Task<bool> UpdateAsync(User user)
    {
        //todo validation
        var userEntity = _mapper.Map<UserEntity>(user);
        return _userRepository.UpdateAsync(userEntity);
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