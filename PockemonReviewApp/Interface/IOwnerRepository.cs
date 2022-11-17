namespace PockemonReviewApp.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfPokemon(int pokemonId);
        ICollection<Pokemon> GetPokemonByOwnerId(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool OwnerExists(int ownerId);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}
