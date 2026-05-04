using PokeShop.Application.DTOs.Storage;

namespace PokeShop.Application.Interfaces
{
    public interface IStorageService
    {
        Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId);

        Task<IEnumerable<TransactionSummaryDto>> GetTransactionsAsync(int userId);

        Task<SellResultDto> SellPokemonAsync(int pokemonId, int userId);
    }
}