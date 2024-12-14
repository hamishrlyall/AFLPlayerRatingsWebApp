﻿using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using AFLPlayerRatingsWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFLPlayerRatingsWebApi.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository ReviewerRepository;
        private readonly IMapper Mapper;

        public ReviewerController( IReviewerRepository _ReviewerRepository, IMapper _Mapper )
        {
            ReviewerRepository = _ReviewerRepository;
            Mapper = _Mapper;
        }

        [HttpGet]
        [ProducesResponseType( 200, Type = typeof( IEnumerable<Reviewer> ) )]
        public IActionResult GetReviewers( )
        {
            var reviewers = Mapper.Map<List<ReviewerDto>>( ReviewerRepository.GetReviewers( ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( reviewers );
        }

        [HttpGet( "{_ReviewerId}" )]
        [ProducesResponseType( 200, Type = typeof( Reviewer ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetReviewer( int _ReviewerId )
        {
            if( !ReviewerRepository.ReviewerExists( _ReviewerId ) )
                return NotFound( );

            var reviewer = Mapper.Map<ReviewerDto>( ReviewerRepository.GetReviewer( _ReviewerId ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( reviewer );
        }

        [HttpGet( "{_ReviewerId}/review" )]
        [ProducesResponseType( 200, Type = typeof( Review ) )]
        [ProducesResponseType( 400 )]
        public IActionResult GetReviewsByReviewer( int _ReviewerId )
        {
            if( !ReviewerRepository.ReviewerExists( _ReviewerId ) )
                return NotFound( );

            var reviews = Mapper.Map<List<ReviewDto>>( ReviewerRepository.GetReviewsByReviewer( _ReviewerId ) );

            if( !ModelState.IsValid )
                return BadRequest( ModelState );

            return Ok( reviews );
        }

        [HttpPost]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 400 )]
        public IActionResult CreateReviewer( [FromBody] ReviewerDto _ReviewerCreate )
        {
            if( _ReviewerCreate == null )
            {
                return BadRequest( ModelState );
            }

            var review = ReviewerRepository.GetReviewers( )
                                           .Where( o => o.FirstName.Trim( ).ToUpper( ) == _ReviewerCreate.FirstName.TrimEnd( ).ToUpper( ) &&
                                                        o.LastName.Trim( ).ToUpper( ) == _ReviewerCreate.LastName.TrimEnd( ).ToUpper( ) )
                                           .FirstOrDefault( );

            if( review != null )
            {
                ModelState.AddModelError( "", "Reviewer already exists" );
                return StatusCode( 422, ModelState );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var reviewerMap = Mapper.Map<Reviewer>( _ReviewerCreate );

            if( !ReviewerRepository.CreateReviewer( reviewerMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong while saving." );
                return StatusCode( 500, ModelState );
            }

            return Ok( "Successfully created." );
        }

        [HttpPut( "{_ReviewerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult UpdateReviewer( int _ReviewerId, [FromBody] ReviewerDto _UpdatedReviewer )
        {
            if( _UpdatedReviewer == null )
            {
                return BadRequest( ModelState );
            }

            if( _ReviewerId != _UpdatedReviewer.Id )
            {
                return BadRequest( ModelState );
            }

            if( !ReviewerRepository.ReviewerExists( _ReviewerId ) )
            {
                return NotFound( );
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var reviewerMap = Mapper.Map<Reviewer>( _UpdatedReviewer );

            if( !ReviewerRepository.UpdateReviewer( reviewerMap ) )
            {
                ModelState.AddModelError( "", "Something went wrong updating reviewer." );
                return StatusCode( 500, ModelState );
            }

            return NoContent( );
        }

        [HttpDelete( "{_ReviewerId}" )]
        [ProducesResponseType( 400 )]
        [ProducesResponseType( 204 )]
        [ProducesResponseType( 404 )]
        public IActionResult DeleteReviewer( int _ReviewerId )
        {
            if( !ReviewerRepository.ReviewerExists( _ReviewerId ) )
            {
                return NotFound( );
            }

            var reviewerToDelete = ReviewerRepository.GetReviewer( _ReviewerId );

            if( !ModelState.IsValid )
            {
                return BadRequest( );
            }

            if( !ReviewerRepository.DeleteReviewer( reviewerToDelete ) )
            {
                ModelState.AddModelError( "", "Something went wrong deleting Reviewer" );
            }

            return NoContent( );
        }
    }
}