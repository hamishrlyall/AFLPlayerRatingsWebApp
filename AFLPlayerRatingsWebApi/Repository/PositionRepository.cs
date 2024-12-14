using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DataContext DataContext;
        public PositionRepository( DataContext _DataContext)
        {
            DataContext = _DataContext;
        }

        public bool PositionExists( int Id )
        {
            return DataContext.Positions.Any( p => p.Id == Id );
        }

        public ICollection<Position> GetPositions( )
        {
            return DataContext.Positions.ToHashSet( );
        }

        public Position GetPosition( int _Id ) 
        {
            var position = DataContext.Positions.Where( p => p.Id == _Id ).FirstOrDefault( );

            if( position == null )
            {
                throw new InvalidOperationException( "Position not found" );
            }

            return position;
        }

        public ICollection<Player> GetPlayersByPosition( int _PositionId )
        {
            return DataContext.PlayerPositions.Where( pp => pp.PositionId == _PositionId ).Select( pos => pos.Player ).ToList( );
        }

        public bool CreatePosition( Position _Position )
        {
            DataContext.Add( _Position );
            return Save( );
        }

        public bool UpdatePosition( Position _Position )
        {
            DataContext.Update( _Position );
            return Save( );
        }

        public bool DeletePosition( Position _Position )
        {
            DataContext.Remove( _Position );
            return Save( );
        }

        public bool Save( )
        {
            var saved = DataContext.SaveChanges( );
            return saved > 0 ? true : false;
        }
    }
}
