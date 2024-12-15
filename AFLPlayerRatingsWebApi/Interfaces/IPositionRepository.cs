using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Interfaces
{
    public interface IPositionRepository
    {
        ICollection<Position> GetPositions( );
        Position GetPosition( int _Id );
        ICollection<Position> GetPositionsBySearchValue( string _SearchValue );
        ICollection<Player> GetPlayersByPosition( int _Id );
        bool PositionExists( int  _Id );
        bool CreatePosition( Position _Position );
        bool UpdatePosition( Position _Position );
        bool DeletePosition( Position _Position );
        bool Save( );
    }
}
