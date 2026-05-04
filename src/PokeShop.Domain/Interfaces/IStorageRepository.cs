namespace PokeShop.Domain.Interfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Pokemon>> GetUserInventoryAsListAsync(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsAsListAsync(int userId);
        Task<Pokemon?> GetPokemonById(int id);
        Task<User?> GetUserById(int id);
        Task<bool> UserExistsByIdAsync(int id);
        Task<bool> UserOwnsSomeInventary(int id);
        Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id);
        Task SaveSellAsync(Transaction transaction, PokemonCenter? pokemonToReturn = null);
    }
}