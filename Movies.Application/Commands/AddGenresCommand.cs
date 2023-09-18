using MediatR;
using Movies.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands
{
    public class AddGenresCommand : IRequest<bool>
    {
        private readonly string dataFileURL;
        public string DataFileURL { get { return dataFileURL; } }
        public AddGenresCommand(string dataFileURL) => this.dataFileURL = dataFileURL;
    }
}
