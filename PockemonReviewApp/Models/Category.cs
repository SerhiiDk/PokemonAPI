namespace PockemonReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // for join table
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
