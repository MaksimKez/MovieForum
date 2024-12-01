using System.Security.Cryptography;
using System.Text;
using MovieForum.BusinessLogic.auth.Interfaces;

namespace MovieForum.BusinessLogic.auth;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateSalt(int workFactor = 12)
    {
        return BCrypt.Net.BCrypt.GenerateSalt(workFactor);
    }

    public string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password, string storedHashedPassword, string storedSalt)
    {
        throw new NotImplementedException();
    }

    public bool VerifyPassword(string password, string storedHashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
    }
}