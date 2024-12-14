namespace AFLPlayerRatingsWebApi.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerPosition> PlayerPositions { get; set; }
    }
}
