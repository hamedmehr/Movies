using MediatR;

namespace Movies.Application.Commands
{
    public class AddMoviesCommand : IRequest<bool>
    {
        private readonly string dataFileURL;
        public string DataFileURL { get { return dataFileURL; } }
        public AddMoviesCommand(string dataFileURL) => this.dataFileURL = dataFileURL;
    }
}
