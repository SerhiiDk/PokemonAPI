namespace PockemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review reviewCreate, int reviewerId, int pokeId )
        {


            var pokemon = _context.Pokemon.Where(p => p.Id == pokeId).FirstOrDefault();

            var reviewer = _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();

            var review = new Review()
            {
                Text = reviewCreate.Text,
                Rating = reviewCreate.Rating,
                Title = reviewCreate.Title, 
                Pokemon = pokemon,
                Reviewer = reviewer
            };
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.Reviews.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewOfPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
