using Movies.Core.Entities;
using Repository;

namespace Movies.Core.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<bool> AddGenresAsync(List<Genre> genres);
        Task<List<Genre>> GetGenresAsync(int count);
    }
}
