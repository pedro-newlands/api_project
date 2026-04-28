using ProjetoPokeShop.DTOs;
<<<<<<< HEAD
=======
using ProjetoPokeShop.Models;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

namespace ProjetoPokeShop.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId);

<<<<<<< HEAD
        Task<SellResultDto> SellPokemonAsync(int userId, int userPokemonId);
=======
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int userId);

        Task<SellResultDto> SellPokemonAsync(int pokemonId, int userId);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    }
}