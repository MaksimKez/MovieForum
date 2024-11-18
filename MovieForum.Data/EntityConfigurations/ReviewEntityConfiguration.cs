using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieForum.Data.Entities;

public class ReviewEntityConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.PublishDate)
            .IsRequired();

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Comments)
            .WithOne(c => c.Review)
            .HasForeignKey(c => c.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}