using System.Security.Cryptography;

namespace MovieForum.Data.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    
    public string? GoogleId { get; set; }
    public string? FullName { get; set; }
}