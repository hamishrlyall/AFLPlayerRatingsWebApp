using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
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
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                var teams = Mapper.Map<List<TeamDto>>( TeamRepository.GetTeams( ) );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = teams;

                return Ok( response );
            }
            catch( Exception ex ) 
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpGet( "{_TeamId}" )]
        [ProducesResponseType( 200, Type = typeof( Team ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetTeam( int _TeamId ) 
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
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

                response.Status = true;
                response.Message = "Success";
                response.Data = team;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpGet( "GetTeamsBySearchValue/{_SearchText}" )]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Team> ) )]
        public IActionResult GetTeamsBySearchValue( string _SearchText )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                var searchedTeams = TeamRepository.GetTeamsBySearchValue( _SearchText ).Select( x => new
                {
                    x.Id,
                    x.Name,

                } ).ToList( );

                response.Status = true;
                response.Message = "Success";
                response.Data = searchedTeams;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }


        [HttpGet( "player/{_TeamId}" )]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Team> ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayersFromATeam( int _TeamId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
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

                response.Status = true;
                response.Message = "Success";
                response.Data = players;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        public IActionResult CreateTeam( [FromBody] TeamDto _TeamCreate )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
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

                response.Status = true;
                response.Message = "Success";
                response.Data = _TeamCreate;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpPut( "{_TeamId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdateTeam( int _TeamId, [FromBody] TeamDto _UpdatedTeam )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
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

                var teamMap = Mapper.Map<Team>( _UpdatedTeam );

                if( !TeamRepository.UpdateTeam( teamMap ) )
                {
                    ModelState.AddModelError( "", "Something went wrong updating team." );
                    return StatusCode( 500, ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = _UpdatedTeam;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpDelete( "{_TeamId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteTeam( int _TeamId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
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
                    return NoContent( );
                }

                response.Status = true;
                response.Message = "Success";

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }
    }
}
