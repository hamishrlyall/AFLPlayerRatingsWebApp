﻿using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFLPlayerRatingsWebApi.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository ReviewRepository;
        private readonly IPlayerRepository PlayerRepository;
        private readonly IReviewerRepository ReviewerRepository;
        private readonly IMapper Mapper;
        public ReviewController( IReviewRepository _ReviewRepository, IMapper _Mapper, IPlayerRepository _PlayerRepository, IReviewerRepository _ReviewerRepository )
        {
            ReviewRepository = _ReviewRepository;
            Mapper = _Mapper;
            PlayerRepository = _PlayerRepository;
            ReviewerRepository = _ReviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Review> ) )]
        public IActionResult GetReviews( )
        {
            var reviews = Mapper.Map<List<ReviewDto>>( ReviewRepository.GetReviews( ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( reviews );
        }

        [HttpGet( "{_ReviewId}" )]
        [ProducesResponseType( 200, Type = typeof( Review ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetReview( int _ReviewId )
        {
            if( !ReviewRepository.ReviewExists( _ReviewId ) )
                return NotFound( );

            var review = Mapper.Map<ReviewDto>( ReviewRepository.GetReview( _ReviewId ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( review );
        }

        [HttpGet( "player/{_PlayerId}" )]
        [ProducesResponseType( 200, Type = typeof( Review ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetReviewsForAPokemon( int _PlayerId )
        {
            var reviews = Mapper.Map<List<ReviewDto>>( ReviewRepository.GetReviewsForPlayer( _PlayerId ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( reviews );
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        public IActionResult CreateReview( [FromQuery] int _ReviewerId, [FromQuery] int _PlayerId, [FromBody] ReviewDto _ReviewCreate )
        {
            if( _ReviewCreate == null )
            {
                return BadRequest( ModelState );
            }

            var review = ReviewRepository.GetReviews( )
                                           .Where( r => r.Title.Trim( ).ToUpper( ) == _ReviewCreate.Title.TrimEnd( ).ToUpper( ) )
                                           .FirstOrDefault( );

            if( review != null )
            {
                ModelState.AddModelError( "", "Review already exists" );
                return StatusCode( 422, ModelState );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var reviewMap = Mapper.Map<Review>( _ReviewCreate );
            reviewMap.Player = PlayerRepository.GetPlayer( _PlayerId );
            reviewMap.Reviewer = ReviewerRepository.GetReviewer( _ReviewerId );

            if( !ReviewRepository.CreateReview( reviewMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong while saving." );
                return StatusCode( 500, ModelState );
            }

            return Ok( "Successfully created." );
        }

        [HttpPut( "{_ReviewId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdateReviewer( int _ReviewId, [FromBody] ReviewDto _UpdatedReview )
        {
            if( _UpdatedReview == null )
            {
                return BadRequest( ModelState );
            }

            if( _ReviewId != _UpdatedReview.Id )
            {
                return BadRequest( ModelState );
            }

            if( !ReviewRepository.ReviewExists( _ReviewId ) )
            {
                return NotFound( );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var reviewMap = Mapper.Map<Review>( _UpdatedReview );

            if( !ReviewRepository.UpdateReview( reviewMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong updating review." );
                return StatusCode( 500, ModelState );
            }

            return NoContent( );
        }

        [HttpDelete( "{_ReviewId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteReview( int _ReviewId )
        {
            if( !ReviewRepository.ReviewExists( _ReviewId ) )
            {
                return NotFound( );
            }

            var reviewToDelete = ReviewRepository.GetReview( _ReviewId );

            if( !ModelState.IsValid )
            {
                return BadRequest( );
            }

            if( !ReviewRepository.DeleteReview( reviewToDelete ) )
            {
                ModelState.AddModelError( "", "Something went wrong deleting Review" );
            }

            return NoContent( );
        }
    }
}