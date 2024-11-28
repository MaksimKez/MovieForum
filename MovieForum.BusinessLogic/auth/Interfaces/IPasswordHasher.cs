namespace MovieForum.BusinessLogic.auth.Interfaces;

public interface IPasswordHasher
{
    string GenerateSalt(int size);
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string storedHashedPassword, string storedSalt);
}