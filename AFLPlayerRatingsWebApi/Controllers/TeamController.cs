using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AFLPlayerRatingsWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFLPlayerRatingsWebApi.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamRepository TeamRepository;
        private readonly IMapper Mapper;

        public TeamController( ITeamRepository _TeamRepository, IMapper _Mapper )
        {
            TeamRepository = _TeamRepository;
            Mapper = _Mapper;
        }

        [HttpGet]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Team>)  )]
        public IActionResult GetTeams( )
        {
            var teams = Mapper.Map<List<TeamDto>>( TeamRepository.GetTeams( ) );

            if( !ModelState.IsValid)
            {
                return BadRequest( ModelState );
            }

            return Ok( teams );
        }

        [HttpGet( "{_TeamId}" )]
        [ProducesResponseType( 200, Type = typeof( Team ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetTeam( int _TeamId ) 
        {
            if( !TeamRepository.TeamExists( _TeamId ) )
            {
                return NotFound( );
            }

            var team = Mapper.Map<TeamDto>( TeamRepository.GetTeam( _TeamId ) );

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            return Ok( team );
        }

        [HttpGet( "player/{_TeamId}" )]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Team> ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayersFromATeam( int _TeamId )
        {
            if( !TeamRepository.TeamExists( _TeamId ) )
            {
                return NotFound( );
            }

            var players = Mapper.Map<List<PlayerDto>>(
                TeamRepository.GetPlayersFromATeam( _TeamId ) );

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            return Ok( players );
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        public IActionResult CreateTeam( [FromBody] TeamDto _TeamCreate )
        {
            if( _TeamCreate == null )
            {
                return BadRequest( ModelState );
            }

            var team = TeamRepository.GetTeams( )
                                           .Where( t => t.Name.Trim( ).ToUpper( ) == _TeamCreate.Name.TrimEnd( ).ToUpper( ) )
                                           .FirstOrDefault( );

            if( team != null )
            {
                ModelState.AddModelError( "", "Team already exists" );
                return StatusCode( 422, ModelState );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var teamMap = Mapper.Map<Team>( _TeamCreate );

            if( !TeamRepository.CreateTeam( teamMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong while saving." );
                return StatusCode( 500, ModelState );
            }

            return Ok( "Successfully created." );
        }

        [HttpPut( "{_TeamId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdateTeam( int _TeamId, [FromBody] TeamDto _UpdatedTeam )
        {
            if( _UpdatedTeam == null )
            {
                return BadRequest( ModelState );
            }

            if( _TeamId != _UpdatedTeam.Id )
            {
                return BadRequest( ModelState );
            }

            if( !TeamRepository.TeamExists( _TeamId ) )
            {
                return NotFound( );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var ownerMap = Mapper.Map<Team>( _UpdatedTeam );

            if( !TeamRepository.UpdateTeam( ownerMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong updating team." );
                return StatusCode( 500, ModelState );
            }

            return NoContent( );
        }

        [HttpDelete( "{_TeamId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteOwner( int _TeamId )
        {
            if( !TeamRepository.TeamExists( _TeamId ) )
            {
                return NotFound( );
            }

            var teamToDelete = TeamRepository.GetTeam( _TeamId );

            if( !ModelState.IsValid )
            {
                return BadRequest( );
            }

            if( !TeamRepository.DeleteTeam( teamToDelete ) )
            {
                ModelState.AddModelError( "", "Something went wrong deleting team" );
            }

            return NoContent( );
        }
    }
}
