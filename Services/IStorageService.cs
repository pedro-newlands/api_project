using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface IStorageService
    {
        Task<List<PokemonDto>> Inventory(int userId);

        Task<SellResultDto> SellPokemon(int userId, int userPokemonId);
    }
}