using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AFLPlayerRatingsWebApi.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext DataContext;

        public ReviewerRepository( DataContext _Context )
        {
            DataContext = _Context;
        }
        public bool ReviewerExists( int _ReviewerId )
        {
            return DataContext.Reviewers.Any( r => r.Id == _ReviewerId );
        }

        public ICollection<Reviewer> GetReviewers( )
        {
            return DataContext.Reviewers.ToList( );
        }

        public Reviewer GetReviewer( int _Id )
        {
            var reviewer = DataContext.Reviewers.Where( r => r.Id == _Id).Include( e => e.Reviews ).FirstOrDefault();

            if( reviewer == null ) 
            {
                throw new InvalidOperationException( $"Reviewer not found" );
            }

            return reviewer;
        }

        public ICollection<Review> GetReviewsByReviewer( int _Id )
        {
            return DataContext.Reviews.Where( r => r.Reviewer.Id == _Id ).ToList( );
        }

        public bool CreateReviewer( Reviewer _Reviewer )
        {
            DataContext.Add( _Reviewer );
            return Save( );
        }
        public bool UpdateReviewer( Reviewer _Reviewer )
        {
            DataContext.Update( _Reviewer );
            return Save( );
        }

        public bool DeleteReviewer( Reviewer _Reviewer )
        {
            DataContext.Remove( _Reviewer );
            return Save( );
        }

        public bool Save( )
        {
            var saved = DataContext.SaveChanges( );
            return saved > 0 ? true : false;
        }
    }
}
