using Data;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Infrastructure.Context;
using Repository;

namespace Movies.Infrastructure.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly MoviesContext context;

        public GenreRepository(MoviesContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> AddGenresAsync(List<Genre> genres)
        {
            foreach (var genre in genres)
            {
                var entity = await context.Genres.Where(x => x.Id == genre.Id).AsTracking().FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Name = genre.Name;
                    entity.Description = genre.Description;
                    entity.IsActive = genre.IsActive;
                    entity.Priority = genre.Priority;
                    entity.LastUpdate = genre.LastUpdate;
                }
                else
                {
                    await context.Genres.AddAsync(genre);
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

        public async Task<List<Genre>> GetGenresAsync(int count)
        {
            var query = await context.Genres
                .Include(x => x.Movies.Where(m => m.IsActive).Take(10).OrderByDescending(m => m.LastUpdate))
                .Where(x => x.IsActive && x.Movies.Where(m => m.IsActive).Count() > 1)
                .OrderBy(x => x.Priority)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
            return query;
        }
    }
}
