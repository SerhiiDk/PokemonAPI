namespace PockemonReviewApp.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewOfPokemon(int pokeId);
        bool ReviewExists(int reviewId);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool CreateReview(Review review, int reviewId, int pokeid);
        bool Save();
    }
}
