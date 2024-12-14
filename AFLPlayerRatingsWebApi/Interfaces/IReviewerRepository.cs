using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers( );
        Reviewer GetReviewer( int _Id );
        ICollection<Review> GetReviewsByReviewer( int _Id );
        bool ReviewerExists( int _ReviewerId );
        bool CreateReviewer( Reviewer _Reviewer );
        bool UpdateReviewer( Reviewer _Reviewer );
        bool DeleteReviewer( Reviewer _Reviewer );
        bool Save( );
    }
}
