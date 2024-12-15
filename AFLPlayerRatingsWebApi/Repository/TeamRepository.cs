using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AFLPlayerRatingsWebApi.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext DataContext;

        public TeamRepository( DataContext _Context ) 
        {
            DataContext = _Context;
        }
        public bool TeamExists( int _TeamId )
        {
            return DataContext.Teams.Any( t => t.Id == _TeamId );
        }
        public ICollection<Team> GetTeams( )
        {
            return DataContext.Teams.OrderBy( t => t.Id ).ToList( );
        }

        public Team GetTeam( int _Id )
        {
            var team = DataContext.Teams.Where( p => p.Id == _Id ).FirstOrDefault( );

            if( team == null )
            {
                throw new InvalidOperationException( $"Team not found" );
            }

            return team;
        }

        public ICollection<Team> GetTeamsBySearchValue( string _SearchValue )
        {
            return DataContext.Teams.Where( x => x.Name.Contains( _SearchValue ) ).ToList( );
        }

        public ICollection<Player> GetPlayersFromATeam( int _TeamId )
        {
            return DataContext.Players.Where( p => p.Team.Id == _TeamId ).ToList( );
        }

        public bool CreateTeam( Team _Team )
        {
            DataContext.Add( _Team );
            return Save( );
        }


        public bool UpdateTeam( Team _Team )
        {
            DataContext.Update( _Team );
            return Save( );
        }

        public bool DeleteTeam( Team _Team )
        {
            DataContext.Remove( _Team );
            return Save( );
        }

        public bool Save( )
        {
            var saved = DataContext.SaveChanges( );
            return saved > 0 ? true : false;
        }
    }
}
