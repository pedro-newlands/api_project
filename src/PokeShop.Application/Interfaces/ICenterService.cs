using PokeShop.Application.DTOs.Center;

namespace PokeShop.Application.Interfaces
{
    public interface ICenterService
    {
        Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemonsAsync();

        Task<BuyResultDto> BuyPokemonAsync(int pokemonCenterId, int userId);

        Task<PokeballDto> BuyPokeballAsync(int userId);
    }
}