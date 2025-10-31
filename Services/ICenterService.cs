using ProjetoPokeShop.DTOs;

namespace ProjetoPokeShop.Services
{
    public interface ICenterService
    {
        Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemons();

        Task<BuyResultDto> BuyPokemon(int pokemonCenterId, int userId);

        Task<PokeballDto> Pokeball(int userId);
    }
}