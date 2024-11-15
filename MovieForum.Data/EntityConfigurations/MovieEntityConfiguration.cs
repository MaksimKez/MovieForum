using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieForum.Data.Entities;

namespace MovieForum.Data.EntityConfigurations;

public class MovieEntityConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(m => m.ShortDescription)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(m => m.ReleaseDate)
            .IsRequired();

        builder.Property(m => m.Rating)
            .IsRequired()
            .HasPrecision(3, 2);

        builder.Property(m => m.AgeLimit)
            .IsRequired();

        builder.HasMany(m => m.Reviews)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId);
    }
}