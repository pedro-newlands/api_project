using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId);

        Task<IEnumerable<Transaction>> GetTransactionsAsync(int userId);

        Task<SellResultDto> SellPokemonAsync(int pokemonId, int userId);
    }
}