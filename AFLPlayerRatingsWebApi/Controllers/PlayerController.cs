using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace AFLPlayerRatingsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository PlayerRepository;
        private readonly IPositionRepository PositionRepository;
        private readonly ITeamRepository TeamRepository;
        private readonly IReviewRepository ReviewRepository;
        private readonly IMapper Mapper;
        public PlayerController( IPlayerRepository _PlayerRepository, IPositionRepository _PositionRepository,
                                 ITeamRepository _TeamRepository, IReviewRepository _ReviewRepository, IMapper _Mapper )
        {
            PlayerRepository = _PlayerRepository;
            PositionRepository = _PositionRepository;
            TeamRepository = _TeamRepository;
            ReviewRepository = _ReviewRepository;
            Mapper = _Mapper;
        }

        [HttpGet]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Player> ) )]
        public IActionResult GetPlayers( )
        {
            var players = Mapper.Map<List<PlayerDto>>( PlayerRepository.GetPlayers( ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( players );
        }

        [HttpGet( "{_PlayerId}" )]
        [ProducesResponseType( 200, Type = typeof( Player ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayer( int _PlayerId )
        {
            if( !PlayerRepository.PlayerExists( _PlayerId ) )
                return NotFound( );

            var player = Mapper.Map<PlayerDto>( PlayerRepository.GetPlayer( _PlayerId ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( player );
        }

        [HttpGet( "{_PlayerId}/rating" )]
        [ProducesResponseType( 200, Type = typeof( decimal ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayerRating( int _PlayerId )
        {
            if( !PlayerRepository.PlayerExists( _PlayerId ) )
                return NotFound( );

            var rating = PlayerRepository.GetPlayerRating( _PlayerId );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( rating );
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType( 400 )]
        public IActionResult CreatePlayer( [FromQuery] int _TeamId, [FromQuery] int _PositionId, [FromBody] PlayerDto _PlayerCreate )
        {
            if( _PlayerCreate == null )
            {
                return BadRequest( ModelState );
            }

            var player = PlayerRepository.GetPlayers( )
                                         .Where( p => p.Name.Trim( ).ToUpper( ) == _PlayerCreate.Name.TrimEnd( ).ToUpper( ) )
                                         .FirstOrDefault( );

            if( player != null )
            {
                ModelState.AddModelError( "", "Player already exists" );
                return StatusCode( 422, ModelState );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var playerMap = Mapper.Map<Player>( _PlayerCreate );

            if( !PlayerRepository.CreatePlayer( _TeamId, _PositionId, playerMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong while saving." );
                return StatusCode( 500, ModelState );
            }

            return Ok( "Successfully created." );
        }

        [HttpPut( "{_PlayerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdatePlayer( int _PlayerId, [FromBody] PlayerDto _UpdatedPlayer ) 
        {
            if( _UpdatedPlayer == null )
            {
                return BadRequest( ModelState );
            }

            if( _PlayerId != _UpdatedPlayer.Id )
            {
                return BadRequest( ModelState );
            }

            if( !PlayerRepository.PlayerExists( _PlayerId ) )
            {
                return NotFound( );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }    

            var playerMap = Mapper.Map<Player>( _UpdatedPlayer );

            if( !PlayerRepository.UpdatePlayer( playerMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong updateing Player." );

                return StatusCode( 500, ModelState );
            }

            return NoContent( );
        }

        [HttpDelete( "{_PlayerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeletePlayer( int _PlayerId )
        {
            if( !PlayerRepository.PlayerExists( _PlayerId ) )
            {
                return NotFound( );
            }

            var reviewsToDelete = ReviewRepository.GetReviewsForPlayer( _PlayerId );
            var playerToDelete = PlayerRepository.GetPlayer( _PlayerId );

            if( !ModelState.IsValid )
            {
                return BadRequest( );
            }

            if( !ReviewRepository.DeleteReviews( reviewsToDelete.ToList( ) ) )
            {
                ModelState.AddModelError( "", "Something went wrong when deleting reviews." );
            }

            if( !PlayerRepository.DeletePlayer( playerToDelete ) )
            {
                ModelState.AddModelError( "", "Something went wrong deleting Pokemon" );
            }

            return NoContent( );
        }
    }
}
