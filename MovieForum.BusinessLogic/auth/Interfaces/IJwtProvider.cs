namespace MovieForum.BusinessLogic.auth.Interfaces;

public interface IJwtProvider
{
    public string GenerateJwtToken(string email, Guid userId, bool isRefreshToken = false);
    public bool ValidateJwtToken(string token);
}