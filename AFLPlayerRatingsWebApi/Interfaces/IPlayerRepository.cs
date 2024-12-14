using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Interfaces
{
    public interface IPlayerRepository
    {
        ICollection<Player> GetPlayers( );
        Player GetPlayer( int _Id );
        Player GetPlayer( string _Name );
        decimal GetPlayerRating( int _PlayerId );
        bool PlayerExists( int _PlayerId );
        bool CreatePlayer( int _TeamId, int _PositionId, Player _Player );
        bool UpdatePlayer( Player _Player );
        bool DeletePlayer( Player _Player );
        bool Save( );
    }
}
