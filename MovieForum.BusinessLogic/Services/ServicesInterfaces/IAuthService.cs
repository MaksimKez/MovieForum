using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(User user);
    Task<string?> LoginAsync(string email, string password);

}