using ProjetoPokeShop.DTOs;

namespace ProjetoPokeShop.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<EngagedPokemonDto>> Inventory(int userId);

        Task<SellResultDto> SellPokemon(int userId, int userPokemonId);
    }
}