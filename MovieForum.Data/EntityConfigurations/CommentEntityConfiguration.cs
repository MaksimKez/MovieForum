using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieForum.Data.Entities;

namespace MovieForum.Data.EntityConfigurations;

public class CommentEntityConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text)
            .HasMaxLength(1000);

        builder.Property(c => c.IsConsonant)
            .IsRequired();

        builder.Property(c => c.PublishedAt);

        builder.HasOne(c => c.Review)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
