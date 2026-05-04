using System.Linq.Expressions;

namespace PokeShop.Domain.Interfaces
{
    public interface IAdminRepository
    {
        // User management
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UserExistsByIdAsync(int id);
        Task<bool> UserExistsByNameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task <User> UpdateUserAsync(User user); 

        // Pokemon management
        Task<IEnumerable<Pokemon>> GetPokemonsAsync();
        Task<Pokemon?> GetPokemonByIdAsync(int id);
        Task<List<Element>> GetElementsByNames(List<Elements> elementsNames);
        Task<bool> PokemonExistsByIdAsync(int id);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        Task <Pokemon> UpdatePokemonAsync(Pokemon pokemon);
        Task DeletePokemonAsync(Pokemon pokemon);

        // Pokemon Center management
        Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id);
        Task<bool> PokemonCenterExistsById(int id);
        Task<PokemonCenter> CreatePokemonCenterAsync(PokemonCenter pokemonCenter);
        Task <PokemonCenter> UpdatePokemonCenterAsync(PokemonCenter pokemonCenter);
        Task DeletePokemonCenterAsync(PokemonCenter pokemonCenter);

        // Transaction management
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByPokemonIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsHistoryAsync(Expression<Func<Transaction, bool>> filter); // Para permitir filtros no Service
        Task CreateTransactionAsync(Transaction transaction);
    }
}