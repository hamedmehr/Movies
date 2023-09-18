using Extensions.Mapper;
using MediatR;
using Movies.Application.DTOs;
using Movies.Application.Queries;
using Movies.Core.Entities;
using Movies.Core.Interfaces;

namespace Movies.Application.Handlers
{
    public partial class GetMoviessQueryHandler : IRequestHandler<GetMoviessQuery, List<MovieDTO>>
    {
        private readonly IMovieRepository movieRepository;
        public GetMoviessQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<List<MovieDTO>> Handle(GetMoviessQuery request, CancellationToken cancellationToken)
        {
            var movies = await movieRepository.GetMoviesAsync(request.GenreId,request.Page,request.Count);
            var movieList = Mapper.Map<List<Movie>, List<MovieDTO>>(movies);
            return movieList;
        }
    }
}
