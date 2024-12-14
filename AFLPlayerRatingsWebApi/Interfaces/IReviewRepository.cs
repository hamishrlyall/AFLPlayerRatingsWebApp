using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews( );
        Review GetReview( int _Id );
        ICollection<Review> GetReviewsForPlayer( int _PlayerId );
        bool ReviewExists( int _ReviewId );
        bool CreateReview( Review _Review );
        bool UpdateReview( Review _Review );
        bool DeleteReview( Review _Review );
        bool DeleteReviews( List<Review> _Reviews );
        bool Save( );
    }
}
