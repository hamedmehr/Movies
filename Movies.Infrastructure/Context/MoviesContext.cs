using Data;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Context
{
    public class MoviesContext : BaseContext
    {
        public MoviesContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
               .HasOne(p => p.Genre)
               .WithMany(s => s.Movies)
               .HasForeignKey(s => s.GenreId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
