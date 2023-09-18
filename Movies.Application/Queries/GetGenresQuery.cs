using MediatR;
using Movies.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries
{
    public class GetGenresQuery : IRequest<List<GenreDTO>>
    {
        private readonly int count;
        public int Count { get { return count; } }
        public GetGenresQuery(int count) => this.count = count;
    }
}
