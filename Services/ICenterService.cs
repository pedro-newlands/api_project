using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface ICenterService
    {
        Task<List<PokemonDto>> GetAvailablePokemons();

        Task<BuyResultDto> BuyPokemon(int pokemonCenterId, int userId);

        Task<PokeballDto> Pokeball(int userId);
    }
}