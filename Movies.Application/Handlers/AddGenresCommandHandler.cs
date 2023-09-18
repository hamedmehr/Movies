using Extensions.Mapper;
using MediatR;
using Movies.Application.Commands;
using Movies.Application.DTOs;
using Movies.Core.Entities;
using Movies.Core.Interfaces;

namespace Movies.Application.Handlers
{
    public partial class AddGenresCommandHandler : IRequestHandler<AddGenresCommand, bool>
    {
        private readonly IGenreRepository genreRepository;
        public AddGenresCommandHandler(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<bool> Handle(AddGenresCommand request, CancellationToken cancellationToken)
        {
            var genres = new List<GenreDTO>();
            var mapper = new Npoi.Mapper.Mapper(request.DataFileURL);
            var workbook = mapper.Workbook;
            var sheet = workbook.GetSheetAt(0);
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                genres.Add(new GenreDTO
                {
                    Id = long.Parse(sheet.GetRow(i).Cells[0].ToString() ?? "0"),
                    Name = sheet.GetRow(i).Cells[1].ToString() ?? string.Empty,
                    IsActive = sheet.GetRow(i).Cells[2].ToString() == "فعال",
                    Description = sheet.GetRow(i).Cells[3].ToString() ?? string.Empty,
                    LastUpdate = DateTime.Parse(sheet.GetRow(i).Cells[4].ToString() ?? DateTime.Now.ToString()),
                    Priority = int.Parse(sheet.GetRow(i).Cells[5].ToString() ?? "0")
                });
            }
            var genreList = Mapper.Map<List<GenreDTO>, List<Genre>>(genres);
            return await genreRepository.AddGenresAsync(genreList);
        }
    }
}
