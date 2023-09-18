using Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands;
using System.Net;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesImportController : ControllerBase
    {
        private readonly ILogger<MoviesImportController> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public MoviesImportController(
            ILogger<MoviesImportController> logger
            , IMediator mediator
            , IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddGenres")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> AddGenres(IFormFile genresData)
        {
            var genresDataUrl = genresData.SaveFile(_configuration.GetValue<string>("ExcelFilesStoragePath") ?? string.Empty);

            if (string.IsNullOrEmpty(genresDataUrl))
                return BadRequest();

            var command = new AddGenresCommand(genresDataUrl);
            var result = await _mediator.Send<bool>(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("AddMovies")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> AddMovies(IFormFile moviesData)
        {
            var moviesDataUrl = moviesData.SaveFile(_configuration.GetValue<string>("ExcelFilesStoragePath") ?? string.Empty);

            if (string.IsNullOrEmpty(moviesDataUrl))
                return BadRequest();

            var command = new AddMoviesCommand(moviesDataUrl);
            var result = await _mediator.Send<bool>(command);

            return Ok(result);
        }
    }
}