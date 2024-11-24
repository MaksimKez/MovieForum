namespace MovieForum.BusinessLogic.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    
    public string? GoogleId { get; set; }
    public string? FullName { get; set; }
}