using MovieForum.BusinessLogic.auth;
using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Services.ServicesInterfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(User user);
    Task<TokensModel?> LoginAsync(string email, string password);
    Task<string?> RefreshAccessTokenAsync(string refreshToken);


}