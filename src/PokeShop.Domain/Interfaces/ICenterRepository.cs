namespace PokeShop.Domain.Interfaces
{
    public interface ICenterRepository
    {
        Task<IEnumerable<PokemonCenter>> GetAvailablePokemonsAsync();
        Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id);
        Task<User?> GetUserByIdAsync(int id);
        Task<List<Pokemon>> GetAvailablePokemonsByRarityAsync(Rarities rarity);
        Task SavePurchaseAsync(Transaction transaction, PokemonCenter? centerToRemove = null);
    }
}