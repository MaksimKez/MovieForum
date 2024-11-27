using MovieForum.Data.Entities.Enums;

namespace MovieForum.BusinessLogic.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public AgeLimitEnum AgeLimit { get; set; }
    public DateTime ReleaseDate { get; set; }
}