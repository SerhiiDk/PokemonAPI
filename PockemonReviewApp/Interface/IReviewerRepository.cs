namespace PockemonReviewApp.Interface
{
    public interface IReviewerRepository
    {

        ICollection<Reviewer> GetReviewers();
        Reviewer GetReview(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewExists(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool Save();
    }
}
