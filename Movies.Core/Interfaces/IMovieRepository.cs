using Movies.Core.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<bool> AddMoviesAsync(List<Movie> movies);
        Task<List<Movie>> GetMoviesAsync(long genreId,int page,int count);
    }
}
