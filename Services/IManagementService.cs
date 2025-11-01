using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;

namespace api_project.Services
{
    public interface IManagementService
    {
        //User management
        Task<ResultDto<IEnumerable<User>>> GetUsers(string superPassword);

        Task<ResultDto<User>> GetUserById(string superPassword, int targetId);

        Task<ResultDto<User>> CreateUser(string superPassword, UserDto dto);

        Task<ResultDto<User>> UpdateUser(string superPassword, int targetId, UpdateUserDto dto);

        Task<ResultDto<User>> DeleteUser(string superPassword, int targetId);

        //Pokemon management
        Task<ResultDto<IEnumerable<Pokemon>>> GetPokemons(string superPassword);

        Task<ResultDto<Pokemon>> GetPokemonById(string superPassword, int targetId);

        Task<ResultDto<Pokemon>> CreatePokemon(string superPassword, PokemonDto dto);

        Task<ResultDto<Pokemon>> UpdatePokemon(string superPassword, int targetId, UpdatePokemonDto dto);

        Task<ResultDto<Pokemon>> DeletePokemon(string superPassword, int targetId);

        //PokemonCenter management
        Task<ResultDto<PokemonCenter>> DeletePokemonCenter(string superPassword, int targetId);

        Task<ResultDto<PokemonCenter>> RestorePokemonCenter(string superPassword, PokemonCenterDto dto);
    }
}