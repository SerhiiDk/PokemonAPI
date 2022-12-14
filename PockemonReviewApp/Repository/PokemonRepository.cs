namespace PockemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int onwnerId, int categoryId, Pokemon pokemon)
        {

            var pokemonOwnerEntity  = _context.Owners.Where( o => o.Id == onwnerId ).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,

            };

            _context.Add(pokemonOwner);


            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
                 
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
            
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Pokemon.Remove(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id)
        {

            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault(); 
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRatting(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Id == pokeId);

            if(review.Count() <=  0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating)/ review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokenmonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);  
        }

        public bool Save()
        {
            var saved =  _context.SaveChanges();

            return saved > 0 ? true : false; 
        }

        public bool UpdatePokemon(int onwnerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == onwnerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,

            };

            _context.PokemonOwners.Update(pokemonOwner);


            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon

            };

            _context.PokemonCategories.Update(pokemonCategory);


            _context.Pokemon.Update(pokemon);
            return Save();
        }
    }
}
