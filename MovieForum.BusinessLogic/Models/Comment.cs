namespace MovieForum.BusinessLogic.Models;

public class Comment
{
    public Guid Id { get; set; }
    public bool IsPositive { get; set; }
    public string Text { get; set; }
    public DateTime PublishedAt { get; set; }

    public Guid UserId { get; set; }
    public Guid ReviewId { get; set; }
}