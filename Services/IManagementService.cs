using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace api_project.Services
{
    public interface IManagementService
    {
        Task<ResultDto<IEnumerable<User>>> GetUsers(string superPassword);

        Task<ResultDto<User>> UpdateUser(string superPassword, int targetId, UserDto dto);

        Task<ResultDto<User>> DeleteUser(string superPassword, int targetId);

        Task<ResultDto<IEnumerable<Pokemon>>> GetPokemons(string superPassword);

        Task<ResultDto<Pokemon>> GetPokemonById(string superPassword, int targetId);

        Task<ResultDto<Pokemon>> CreatePokemon(string superPassword, PokemonDto dto);

        Task<ResultDto<Pokemon>> UpdatePokemon(string superPassword, int targetId, UpdatePokemonDto dto);

        Task<ResultDto<Pokemon>> DeletePokemon(string superPassword, int targetId);

        Task<ResultDto<PokemonCenter>> DeletePokemonCenter(string superPassword, int targetId);

        Task<ResultDto<PokemonCenter>> RestorePokemonCenter(string superPassword, int targetId);
    }
}