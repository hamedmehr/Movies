using Data;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Infrastructure.Context;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly MoviesContext context;

        public MovieRepository(MoviesContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> AddMoviesAsync(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                var entity = await context.Movies.Where(x => x.Id == movie.Id).AsTracking().FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Name = movie.Name;
                    entity.IsActive = movie.IsActive;
                    entity.GenreId = movie.GenreId;
                    entity.Description = movie.Description;
                    entity.LastUpdate = movie.LastUpdate;
                }
                else
                {
                    await context.Movies.AddAsync(movie);
                }
            }
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Movie>> GetMoviesAsync(long genreId, int page, int count)
        {
            var query = await context.Movies
                .Where(x => x.GenreId == genreId && x.IsActive)
                .OrderByDescending(x => x.LastUpdate)
                .Skip((page - 1) * count)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
            return query;
        }
    }
}
