<<<<<<< HEAD
=======
using api_project.DTOs.Management;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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
<<<<<<< HEAD
        Task<ResultDto<PokemonCenter>> DeletePokemonCenterAsync(string superPassword, int targetId);

        Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(string superPassword, PokemonCenter pokemonCenter);
=======
        Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(string superPassword, PokemonCenterDto dto);

        Task<ResultDto<PokemonCenter>> UpdatePokemonCenterMarketPriceAsync(string superPassword, int targetId, UpdatePriceDto dto);

        Task<ResultDto<PokemonCenter>> DeletePokemonCenterAsync(string superPassword, int targetId);

        //Transaction management
        Task<ResultDto<IEnumerable<Transaction>>> GetAllTransactionsAsync(string superPassword);

        Task<ResultDto<Transaction>> GetTransactionByIdAsync(string superPassword, int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByUserIdAsync(string superPassword, int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByPokemonIdAsync(string superPassword, int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsHistoryAsync(string superPassword, int? year = null, int? month = null, int? day = null);

        Task<ResultDto<Transaction>> DeleteTransactionAsync(string superPassword, int targetId);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    }
}