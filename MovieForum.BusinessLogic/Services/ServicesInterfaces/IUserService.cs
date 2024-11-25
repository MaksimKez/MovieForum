using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<Guid> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
}