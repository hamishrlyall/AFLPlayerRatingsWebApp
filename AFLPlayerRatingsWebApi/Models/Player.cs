namespace AFLPlayerRatingsWebApi.Models
{
    public class Player
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Headshot { get; set; }
        public required Team Team { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<PlayerPosition>? PlayerPositions { get; set; }
    }
}
