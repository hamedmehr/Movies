using Extensions.Mapper;
using MediatR;
using Movies.Application.DTOs;
using Movies.Application.Queries;
using Movies.Core.Entities;
using Movies.Core.Interfaces;

namespace Movies.Application.Handlers
{
    public partial class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<GenreDTO>>
    {
        private readonly IGenreRepository genreRepository;
        public GetGenresQueryHandler(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<List<GenreDTO>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await genreRepository.GetGenresAsync(request.Count);
            var genreList = Mapper.Map<List<Genre>, List<GenreDTO>>(genres);
            return genreList;
        }
    }
}
