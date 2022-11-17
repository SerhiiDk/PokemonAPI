

namespace PockemonReviewApp.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRatting(int pokeId);
        bool PokenmonExists(int pokeId);
        bool CreatePokemon(int onwnerId, int categoryId, Pokemon pokemon);
        bool UpdatePokemon(int onwnerId, int categoryId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();
    }
}
