using Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands;
using Movies.Application.DTOs;
using Movies.Application.Queries;
using System.Net;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesReportController : ControllerBase
    {
        private readonly ILogger<MoviesReportController> _logger;
        private readonly IMediator _mediator;

        public MoviesReportController(
            ILogger<MoviesReportController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetGenres")]
        [ProducesResponseType(typeof(List<GenreDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres(int count = 6)
        {
            var query = new GetGenresQuery(count);
            var result = await _mediator.Send<List<GenreDTO>>(query);

            if (result == null || result.Count == 0) return NoContent();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMovies")]
        [ProducesResponseType(typeof(List<MovieDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<List<MovieDTO>>> GetMovies(long genreId, int page = 1, int count = 10)
        {
            var query = new GetMoviessQuery(genreId, page, count);
            var result = await _mediator.Send<List<MovieDTO>>(query);

            if (result == null || result.Count == 0) return NoContent();

            return Ok(result);
        }
    }
}