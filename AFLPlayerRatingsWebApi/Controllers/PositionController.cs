using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
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
            var positions = Mapper.Map<List<PositionDto>>( PositionRepository.GetPositions( ) );

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            return Ok( positions );
        }

        [HttpGet( "{_PositionId}" )]
        [ProducesResponseType( 200, Type = typeof( Position ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetPosition( int _PositionId ) 
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

            return Ok( position );
        }

        [HttpGet( "player/{_PositionId}" )]
        [ProducesResponseType(200, Type = typeof( IEnumerable<Position>) )]
        [ProducesResponseType(400)]
        public IActionResult GetPlayerByPosition( int _PositionId )
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

            return Ok( players );
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        public IActionResult CreatePosition( [FromBody] PositionDto _PositionCreate )
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

            return Ok( "Successfully created." );
        }

        [HttpPut( "{_PositionId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdatePosition( int _PositionId, [FromBody] PositionDto _UpdatedPosition )
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

            var categoryMap = Mapper.Map<Position>( _UpdatedPosition );

            if( !PositionRepository.UpdatePosition( categoryMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong updating category." );
                return StatusCode( 500, ModelState );
            }

            return NoContent( );
        }

        [HttpDelete( "{_PositionId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteCategory( int _PositionId )
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

            return NoContent( );
        }
    }
}
