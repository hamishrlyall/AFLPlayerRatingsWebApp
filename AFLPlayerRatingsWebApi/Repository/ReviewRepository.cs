using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Models;
using System.Security.Cryptography;

namespace AFLPlayerRatingsWebApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext DataContext;

        public ReviewRepository( DataContext _Context )
        {
            DataContext = _Context;
        }
        public bool ReviewExists( int _ReviewId )
        {
            return DataContext.Reviews.Any( r => r.Id == _ReviewId );
        }
        public ICollection<Review> GetReviews( )
        {
            return DataContext.Reviews.ToList( );
        }

        public Review GetReview( int _Id )
        {
            var review = DataContext.Reviews.Where( r => r.Id == _Id ).FirstOrDefault( );

            if( review == null )
            {
                throw new InvalidOperationException( $"Review not found." );
            }

            return review;
        }

        public ICollection<Review> GetReviewsForPlayer( int _PlayerId )
        {
            return DataContext.Reviews.Where( r => r.Player.Id == _PlayerId ).ToList( );
        }

        public bool CreateReview( Review _Review )
        {
            DataContext.Add( _Review );
            return Save( );
        }

        public bool UpdateReview( Review _Review )
        {
            DataContext.Update( _Review );
            return Save( );
        }

        public bool DeleteReview( Review _Review )
        {
            DataContext.Remove( _Review );
            return Save( );
        }

        public bool DeleteReviews( List<Review> _Reviews )
        {
            DataContext.RemoveRange( _Reviews );
            return Save( );
        }

        public bool Save( )
        {
            var saved = DataContext.SaveChanges( );
            return saved > 0 ? true : false;
        }
    }
}
