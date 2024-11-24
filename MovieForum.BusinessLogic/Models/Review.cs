using MovieForum.Data.Entities.Enums;

namespace MovieForum.BusinessLogic.Models;

public class Review
{
    public Guid Id { get; set; }
    
    public RatingEnum Rating { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime PublishDate { get; set; }
    
    public Guid MovieId { get; set; }
    public Guid UserId { get; set; }

}