using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AFLPlayerRatingsWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.InteropServices;

namespace AFLPlayerRatingsWebApi.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly IPositionRepository PositionRepository;
        private readonly IMapper Mapper;

        public PositionController( IPositionRepository _PositionRepository, IMapper _Mapper )
        {
            PositionRepository = _PositionRepository;
            Mapper = _Mapper;
        }

        [HttpGet]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Position> ) )]
        public IActionResult GetPositions( )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                var positions = Mapper.Map<List<PositionDto>>( PositionRepository.GetPositions( ) );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = new { positions };

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpGet( "{_PositionId}" )]
        [ProducesResponseType( 200, Type = typeof( Position ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPosition( int _PositionId ) 
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( !PositionRepository.PositionExists( _PositionId ) )
                {
                    return NotFound( );
                }

                var position = Mapper.Map<PositionDto>( PositionRepository.GetPosition( _PositionId ) );

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = new { position };
                return Ok( response );
            }
            catch( Exception ex )
            {
                // TODO: do logging exceptions
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpGet( "GetPositionBySearchValue/{_SearchText}" )]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Position> ) )]
        public IActionResult GetPositionBySearchValue( string _SearchText )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                var searchedTeams = PositionRepository.GetPositionsBySearchValue( _SearchText ).Select( x => new
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

        [HttpGet( "player/{_PositionId}" )]
        [ProducesResponseType(200, Type = typeof( IEnumerable<Position>) )]
        [ProducesResponseType(400)]
        public IActionResult GetPlayerByPosition( int _PositionId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( !PositionRepository.PositionExists( _PositionId ) )
                {
                    return NotFound( );
                }

                var players = Mapper.Map<List<PlayerDto>>(
                    PositionRepository.GetPlayersByPosition( _PositionId ) );

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
        public IActionResult CreatePosition( [FromBody] PositionDto _PositionCreate )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( _PositionCreate == null )
                {
                    return BadRequest( ModelState );
                }

                var position = PositionRepository.GetPositions( )
                                               .Where( c => c.Name.Trim( ).ToUpper( ) == _PositionCreate.Name.TrimEnd( ).ToUpper( ) )
                                               .FirstOrDefault( );

                if( position != null )
                {
                    ModelState.AddModelError( "", "Position already exists" );
                    return StatusCode( 422, ModelState );
                }

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                var positionMap = Mapper.Map<Position>( _PositionCreate );

                if( !PositionRepository.CreatePosition( positionMap ) )
                {
                    ModelState.AddModelError( "", "Something went wrong while saving." );
                    return StatusCode( 500, ModelState );
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = _PositionCreate;

                return Ok( response );
            }
            catch( Exception ex )
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpPut( "{_PositionId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdatePosition( int _PositionId, [FromBody] PositionDto _UpdatedPosition )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( _UpdatedPosition == null )
                {
                    return BadRequest( ModelState );
                }

                if( _PositionId != _UpdatedPosition.Id )
                {
                    return BadRequest( ModelState );
                }

                if( !PositionRepository.PositionExists( _PositionId ) )
                {
                    return NotFound( );
                }

                if( !ModelState.IsValid )
                {
                    return BadRequest( ModelState );
                }

                var positionMap = Mapper.Map<Position>( _UpdatedPosition );

                if( !PositionRepository.UpdatePosition( positionMap ) )
                {
                    ModelState.AddModelError( "", "Something went wrong updating category." );
                    return StatusCode( 500, ModelState );
                }


                response.Status = true;
                response.Message = "Success";
                response.Data = _UpdatedPosition;

                return Ok( response );
            }
            catch( Exception ex )
            {
                // TODO: do logging exceptions
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }

        [HttpDelete( "{_PositionId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteCategory( int _PositionId )
        {
            BaseResponseModel response = new BaseResponseModel( );
            try
            {
                if( !PositionRepository.PositionExists( _PositionId ) )
                {
                    return NotFound( );
                }

                var positionToDelete = PositionRepository.GetPosition( _PositionId );

                if( !ModelState.IsValid )
                {
                    return BadRequest( );
                }

                if( !PositionRepository.DeletePosition( positionToDelete ) )
                {
                    ModelState.AddModelError( "", "Something went wrong deleting position" );
                }

                response.Status = true;
                response.Message = "Success";

                return Ok( response );
            }
            catch( Exception ex )
            {
                // TODO: do logging exceptions
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest( response );
            }
        }
    }
}
