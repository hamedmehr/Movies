using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Movies.Core.Interfaces;
using Movies.Infrastructure.Context;
using Movies.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Movies.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesConnection")));
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movies API",
                    Description = "Fadak Movies Code Challenge"
                });
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Movies.Application")));
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddAutoMapper(typeof(Program));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.MapControllers();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.Run();
        }
    }
}