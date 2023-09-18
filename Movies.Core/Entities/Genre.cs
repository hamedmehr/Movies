using Data;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities
{
    public class Genre : BaseEntity
    {
        [MaxLength(200)]
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual ICollection<Movie> Movies { get; set;}
    }
}
