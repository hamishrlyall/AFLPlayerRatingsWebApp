namespace AFLPlayerRatingsWebApi.Models
{
    public class PlayerPosition
    {
        public PlayerPosition( )
        {

        }
        public int PlayerId { get; set; }
        public int PositionId { get; set; }
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
