using System.Security.Cryptography;
using MovieForum.BusinessLogic.auth.Interfaces;

namespace MovieForum.BusinessLogic.auth;

public class PasswordHasher : IPasswordHasher
{
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public string GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(HashSize);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string storedHashedPassword, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var storedHash = Convert.FromBase64String(storedHashedPassword);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var hashToVerify = pbkdf2.GetBytes(HashSize);
        return CryptographicOperations.FixedTimeEquals(hashToVerify, storedHash);
    }
}