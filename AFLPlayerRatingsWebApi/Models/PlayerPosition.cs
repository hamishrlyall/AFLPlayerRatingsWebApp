namespace AFLPlayerRatingsWebApi.Models
{
    public class PlayerPosition
    {
        public PlayerPosition( )
        {

        }
        public int PlayerId { get; set; }
        public int PositionId { get; set; }
        public required Player Player { get; set; }
        public required Position Position { get; set; }
    }
}
