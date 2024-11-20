using System.Dynamic;
using MovieForum.Data.Entities;

namespace MovieForum.Data.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<UserEntity>> GetByUsernameAsync(string username);
    Task<UserEntity> GetByEmailAsync(string email);
    
    Task AddAsync(UserEntity user);
    Task<bool> UpdateAsync(UserEntity user);
    Task<bool> DeleteAsync(UserEntity user);
}