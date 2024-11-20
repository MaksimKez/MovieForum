using MovieForum.Data.Entities.Enums;

namespace MovieForum.Data.Entities;

public class ReviewEntity
{
    public Guid Id { get; set; }
    
    // overall movie rating from review by author
    public RatingEnum Rating { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime PublishDate { get; set; }
    
    public Guid MovieId { get; set; }
    public MovieEntity Movie { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public IEnumerable<CommentEntity> Comments { get; set; }
}