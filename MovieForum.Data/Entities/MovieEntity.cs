using MovieForum.Data.Entities.Enums;

namespace MovieForum.Data.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Rating { get; set; }
    public AgeLimitEnum AgeLimit { get; set; }
    
    public IEnumerable<ReviewEntity> Reviews { get; set; }
}