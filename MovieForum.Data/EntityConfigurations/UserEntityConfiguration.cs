using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieForum.Data.Entities;

namespace MovieForum.Data.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.CreatedAt).IsRequired();
        
        builder.HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(255);

        builder.Property(u => u.PasswordSalt)
            .HasMaxLength(255);

        builder.Property(u => u.GoogleId)
            .HasMaxLength(255);

        builder.Property(u => u.FullName)
            .HasMaxLength(255);
    }
}
