using Extensions.Mapper;
using MediatR;
using Movies.Application.Commands;
using Movies.Application.DTOs;
using Movies.Core.Entities;
using Movies.Core.Interfaces;

namespace Movies.Application.Handlers
{
    public partial class AddGenresCommandHandler
    {
        public class AddMoviesCommandHandler : IRequestHandler<AddMoviesCommand, bool>
        {
            private readonly IMovieRepository movieRepository;
            public AddMoviesCommandHandler(IMovieRepository movieRepository)
            {
                this.movieRepository = movieRepository;
            }

            public async Task<bool> Handle(AddMoviesCommand request, CancellationToken cancellationToken)
            {
                var movies = new List<MovieDTO>();
                var mapper = new Npoi.Mapper.Mapper(request.DataFileURL);
                var workbook = mapper.Workbook;
                var sheet = workbook.GetSheetAt(0);
                for (var i = 1; i <= sheet.LastRowNum; i++)
                {
                    movies.Add(new MovieDTO
                    {
                        Id = long.Parse(sheet.GetRow(i).Cells[0].ToString().Replace(",", "") ?? "0"),
                        Name = sheet.GetRow(i).Cells[1].ToString() ?? string.Empty,
                        IsActive = sheet.GetRow(i).Cells[2].ToString() == "فعال",
                        GenreId = long.Parse(sheet.GetRow(i).Cells[3].ToString().Replace(",", "") ?? "0"),
                        Description = sheet.GetRow(i).Cells[4].ToString() ?? string.Empty,
                        LastUpdate = DateTime.Parse(sheet.GetRow(i).Cells[5].ToString() ?? DateTime.Now.ToString())
                    });
                }
                var movieList = Mapper.Map<List<MovieDTO>, List<Movie>>(movies);
                return await movieRepository.AddMoviesAsync(movieList);
            }
        }
    }
}
