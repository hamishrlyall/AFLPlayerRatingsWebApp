using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Interfaces
{
    public interface ITeamRepository
    {
        ICollection<Team> GetTeams( );
        Team GetTeam( int _Id );
        ICollection<Player> GetPlayersFromATeam( int _TeamId );
        bool TeamExists( int _TeamId );
        bool CreateTeam( Team _Team );
        bool UpdateTeam( Team _Team );
        bool DeleteTeam( Team _Team );
        bool Save( );
    }
}
