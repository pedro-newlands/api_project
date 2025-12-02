using ProjetoPokeShop.DTOs;

namespace ProjetoPokeShop.Services
{
    public interface ICenterService
    {
        Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemonsAsync();

        Task<BuyResultDto> BuyPokemonAsync(int pokemonCenterId, int userId);

        Task<PokeballDto> BuyPokeballAsync(int userId);
    }
}