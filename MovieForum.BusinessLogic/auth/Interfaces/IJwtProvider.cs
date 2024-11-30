namespace MovieForum.BusinessLogic.auth.Interfaces;

public interface IJwtProvider
{
    string GenerateJwtToken(string email, Guid userId);
    bool ValidateJwtToken(string token);
}