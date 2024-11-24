using Microsoft.EntityFrameworkCore;
using MovieForum.BusinessLogic.Helpers.Mappers;
using MovieForum.BusinessLogic.Services;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.Data;
using MovieForum.Data.Interfaces;
using MovieForum.DataAccess.Repositories;

namespace MovieForum;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("ExpenseTrackerDb"));
        });

        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        builder.Services.AddAutoMapper(typeof(CommentMapperProfile));

        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IMovieService, MovieService>();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}