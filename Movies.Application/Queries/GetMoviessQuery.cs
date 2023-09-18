using MediatR;
using Movies.Application.DTOs;

namespace Movies.Application.Queries
{
    public class GetMoviessQuery : IRequest<List<MovieDTO>>
    {
        private readonly long genreId;
        private readonly int page;
        private readonly int count;
        public long GenreId { get { return genreId; } }
        public int Page { get { return page; } }
        public int Count { get { return count; } }
        public GetMoviessQuery(long genreId, int page, int count)
        {
            this.genreId = genreId;
            this.page = page;
            this.count = count;
        }
    }
}
