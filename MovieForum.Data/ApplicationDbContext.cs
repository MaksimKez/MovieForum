using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MovieForum.Data.Entities;

namespace MovieForum.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}