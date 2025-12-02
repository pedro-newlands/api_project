using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface IAdminService
    {
        //User management
        Task<ResultDto<IEnumerable<User>>> GetAllUsersAsync(string superPassword);

        Task<ResultDto<User>> GetUserByIdAsync(string superPassword, int targetId);

        Task<ResultDto<User>> CreateUserAsync(string superPassword, UserDto dto);

        Task<ResultDto<User>> UpdateUserAsync(string superPassword, int targetId, UpdateUserDto dto);

        Task<ResultDto<User>> DeleteUserAsync(string superPassword, int targetId);

        //Pokemon management
        Task<ResultDto<IEnumerable<Pokemon>>> GetAllPokemonsAsync(string superPassword);

        Task<ResultDto<Pokemon>> GetPokemonByIdAsync(string superPassword, int targetId);

        Task<ResultDto<Pokemon>> CreatePokemonAsync(string superPassword, PokemonDto dto);

        Task<ResultDto<Pokemon>> UpdatePokemonAsync(string superPassword, int targetId, UpdatePokemonDto dto);

        Task<ResultDto<Pokemon>> DeletePokemonAsync(string superPassword, int targetId);

        //PokemonCenter management
        Task<ResultDto<PokemonCenter>> DeletePokemonCenterAsync(string superPassword, int targetId);

        Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(string superPassword, PokemonCenter pokemonCenter);
    }
}