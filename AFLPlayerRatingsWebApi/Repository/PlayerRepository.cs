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

        public ICollection<Player> GetAllPlayers( )
        {
            return DataContext.Players.OrderBy( p => p.Id ).ToList( );
        }

        public ICollection<Player> GetPlayers( int _PageIndex, int _PageSize )
        {
            return DataContext.Players.Include( t => t.Team ).Skip( _PageIndex * _PageSize ).Take( _PageSize ).OrderBy( p => p.Id ).ToList( );
        }

        public int GetPlayerCount( )
        {
            return DataContext.Players.Count( );
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

        public bool CreatePlayer( int _TeamId, Player _Player )
        {
            var team = DataContext.Teams.Where( t => t.Id == _TeamId ).FirstOrDefault( );
            if( team == null )
            {
                throw new InvalidOperationException( $"Team not found" );
            }

            _Player.Team = team;

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
