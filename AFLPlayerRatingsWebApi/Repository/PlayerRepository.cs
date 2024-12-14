using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AFLPlayerRatingsWebApi.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext DataContext;

        public PlayerRepository( DataContext _Context )
        {
            DataContext = _Context;
        }

        public bool PlayerExists( int _PlayerId )
        {
            return DataContext.Players.Any( p => p.Id == _PlayerId );
        }

        public ICollection<Player> GetPlayers( )
        {
            return DataContext.Players.OrderBy( p => p.Id ).ToList( );
        }

        public Player GetPlayer( int _Id )
        {
            var player = DataContext.Players.Where( p => p.Id == _Id ).FirstOrDefault( );

            if( player == null )
            {
                throw new InvalidOperationException( $"Player not found" );
            }

            return player;
        }

        public Player GetPlayer( string _Name )
        {
            var player = DataContext.Players.Where( p => p.Name == _Name ).FirstOrDefault( );

            if( player == null )
            {
                throw new InvalidOperationException( $"Player not found" );
            }

            return player;
        }
        public decimal GetPlayerRating( int _PlayerId )
        {
            var reviews = DataContext.Reviews.Where( p => p.Player.Id == _PlayerId );

            if( reviews.Count( ) <= 0 )
            {
                return 0;
            }

            return ( ( decimal ) reviews.Sum( r => r.Rating ) / reviews.Count( ) );
        }

        public bool CreatePlayer( int _TeamId, int _PositionId, Player _Player )
        {
            var team = DataContext.Teams.Where( t => t.Id == _TeamId ).FirstOrDefault( );
            if( team == null ) 
            {
                throw new InvalidOperationException( $"Team not found" );
            }

            _Player.Team = team;

            var position = DataContext.Positions.Where( p => p.Id == _PositionId ).FirstOrDefault();
            if( position == null )
            {
                throw new InvalidOperationException( $"Position not found" );
            }

            var playerPosition = new PlayerPosition( )
            {
                Player = _Player,
                Position = position
            };

            DataContext.Add( playerPosition );

            DataContext.Add( _Player );
            return Save( );
        }

        public bool UpdatePlayer( Player _Player )
        {
            DataContext.Update( _Player );
            return Save( );
        }

        public bool DeletePlayer( Player _Player )
        {
            DataContext.Remove( _Player );
            return Save( );
        }

        public bool Save( )
        {
            var saved = DataContext.SaveChanges( );
            return saved > 0 ? true : false;
        }
    }
}
