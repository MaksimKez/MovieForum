namespace MovieForum.BusinessLogic.auth.Interfaces;

public interface IPasswordHasher
{
    string GenerateSalt(int workFactor = 12);
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string storedHashedPassword, string storedSalt);
    bool VerifyPassword(string password, string storedHashedPassword);
}