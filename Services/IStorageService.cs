using ProjetoPokeShop.DTOs;

namespace ProjetoPokeShop.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId);

        Task<SellResultDto> SellPokemonAsync(int userId, int userPokemonId);
    }
}