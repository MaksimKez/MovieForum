using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieForum.BusinessLogic.auth;
using MovieForum.BusinessLogic.auth.Interfaces;
using MovieForum.BusinessLogic.Helpers.Mappers;
using MovieForum.BusinessLogic.Services;
using MovieForum.BusinessLogic.Services.ServicesInterfaces;
using MovieForum.BusinessLogic.Validators;
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
        
        builder.Services.AddAutoMapper(typeof(CommentMapperProfile)
            ,typeof(MovieMapperProfile), typeof(ReviewMapperProfile), typeof(UserMapperProfile));

        builder.Services.AddTransient<UserValidator>();
        builder.Services.AddTransient<CommentValidator>();
        builder.Services.AddTransient<MovieValidator>();
        builder.Services.AddTransient<ReviewValidator>();
        
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        
        builder.Services.AddScoped<IAuthService, AuthService>();
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345"u8.ToArray()),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Write Jwt token in format: Bearer {token}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        var app = builder.Build();
        
        app.MapControllers();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.Run();
    }
}