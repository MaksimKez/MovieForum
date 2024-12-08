using System.Dynamic;
using MovieForum.Data.Entities;

namespace MovieForum.Data.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<UserEntity>> GetByUsernameAsync(string username);
    Task<UserEntity?> GetByEmailAsync(string email);
    
    Task<Guid> AddAsync(UserEntity user);
    Task<bool> UpdateAsync(UserEntity user);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddRefreshTokenAsync(Guid userId, string refreshToken);
    Task<string?> GetRefreshTokenAsync(Guid userId);
}