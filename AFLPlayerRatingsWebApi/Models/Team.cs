namespace AFLPlayerRatingsWebApi.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomeGround { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
