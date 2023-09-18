using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.DTOs
{
    public class GenreDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime LastUpdate { get; set; }
        public IEnumerable<MovieDTO> Movies { get; set; }
    }
}
