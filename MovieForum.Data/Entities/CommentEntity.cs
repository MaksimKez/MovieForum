namespace MovieForum.Data.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    public bool IsPositive { get; set; }
    public string Text { get; set; }
    public DateTime PublishedAt { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid ReviewId { get; set; }
    public ReviewEntity Review { get; set; }
}