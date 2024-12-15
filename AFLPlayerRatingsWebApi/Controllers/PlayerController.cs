using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;

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
        public IActionResult GetPlayers( int _PageIndex = 0, int _PageSize = 10 )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                var playerCount = PlayerRepository.GetPlayerCount( );
                var players = Mapper.Map<List<PlayerDto>>( PlayerRepository.GetPlayers( _PageIndex, _PageSize ) );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                foreach( var player in players ) 
                { 
                    player.FormattedBirthdate = player.BirthDate.ToString( "dd/MM/yyyy" );
                    player.Rating = PlayerRepository.GetPlayerRating( player.Id ).ToString("F");
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = new {Players =  players, Count = playerCount };

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpGet( "{_PlayerId}" )]
        [ProducesResponseType( 200, Type = typeof( Player ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayer( int _PlayerId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( !PlayerRepository.PlayerExists( _PlayerId ) )
                {
                    return NotFound( );
                }

                var player = Mapper.Map<PlayerDto>( PlayerRepository.GetPlayer( _PlayerId ) );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = new { player };

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }

        }

        [HttpGet( "{_PlayerId}/rating" )]
        [ProducesResponseType( 200, Type = typeof( decimal ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPlayerRating( int _PlayerId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( !PlayerRepository.PlayerExists( _PlayerId ) )
                {
                    return NotFound( );
                }

                var rating = PlayerRepository.GetPlayerRating( _PlayerId );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = new { rating };

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
        [ProducesResponseType(204)]
        [ProducesResponseType( 400 )]
        public IActionResult CreatePlayer( [FromQuery] int _TeamId, [FromBody]PlayerToCreateDto _PlayerCreate )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( _PlayerCreate == null )
                {
                    return BadRequest( ModelState );
                }

                var player = PlayerRepository.GetAllPlayers( )
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

                if( !PlayerRepository.CreatePlayer( _TeamId, playerMap ) )
                {
                    ModelState.AddModelError( "", "Something went wrong while saving." );
                    return StatusCode( 500, ModelState );
                }

                response.Status = true;
                response.Message = "Created Successfully";
                response.Data = _PlayerCreate;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpPut( "{_PlayerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdatePlayer( int _PlayerId, [FromBody] PlayerDto _UpdatedPlayer ) 
        {
            BaseResponseModel response = new BaseResponseModel( );

            try
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
                    ModelState.AddModelError( "", "Something went wrong updating Player." );

                    return StatusCode( 500, ModelState );
                }

                response.Status = true;
                response.Message = "Updated Successfully";
                response.Data = _UpdatedPlayer;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong updating Player";

                return BadRequest( response );
            }
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        [Route( "upload-player-headshot" )]
        public async Task<IActionResult> UploadPlayerHeadShot( IFormFile _ImageFile )
        {
            try
            {
                var fileName = ContentDispositionHeaderValue.Parse( _ImageFile.ContentDisposition ).FileName.TrimStart( '\"' ).TrimEnd( '\"' );
                string newPath = @"E:\to-delete";

                if( !Directory.Exists( newPath ) )
                {
                    Directory.CreateDirectory( newPath );
                }

                string[ ] allowedImageExtensions = new string[ ] { ".jpg", ".jpeg", ".png" };

                if( !allowedImageExtensions.Contains( Path.GetExtension( fileName ) ) )
                {
                    return BadRequest( new BaseResponseModel
                    {
                        Status = false,
                        Message = "Only .jpg, .jpeg and .png type files are allowed.",
                    } );
                }

                string newFileName = Guid.NewGuid( ) + Path.GetExtension( fileName );
                string fullFilePath = Path.Combine( newPath, newFileName );

                using( var stream = new FileStream( fullFilePath, FileMode.Create ) )
                {
                    await _ImageFile.CopyToAsync( stream );
                }

                return Ok( new { ProfileImage = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/StaticFiles/{newFileName}" } );
            }
            catch( Exception ex )
            {
                return BadRequest( new BaseResponseModel
                {
                    Status = false,
                    Message = "Error Occured"
                } );
            }
        }

        [HttpDelete( "{_PlayerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeletePlayer( int _PlayerId )
        {
            BaseResponseModel response = new BaseResponseModel( );

            try
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
                    ModelState.AddModelError( "", "Something went wrong deleting Player" );
                }

                response.Status = true;
                response.Message = "Deleted successfully";

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong deleting Player";

                return BadRequest( response );
            }
        }
    }
}
