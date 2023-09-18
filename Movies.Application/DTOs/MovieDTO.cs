namespace Movies.Application.DTOs
{
    public class MovieDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long GenreId { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
