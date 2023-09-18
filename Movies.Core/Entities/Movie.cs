using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Entities
{
    public class Movie : BaseEntity
    {
        [MaxLength(200)]
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public Genre Genre { get; set; }
        public long GenreId { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
