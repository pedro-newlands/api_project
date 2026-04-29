using api_project.DTOs.Management;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface IAdminService
    {
        //User management
        Task<ResultDto<IEnumerable<User>>> GetAllUsersAsync();

        Task<ResultDto<User>> GetUserByIdAsync(int targetId);

        Task<ResultDto<User>> CreateUserAsync(UserDto dto);

        Task<ResultDto<User>> UpdateUserAsync(int targetId, UpdateUserDto dto);

        Task<ResultDto<User>> DeleteUserAsync(int targetId);

        //Pokemon management
        Task<ResultDto<IEnumerable<Pokemon>>> GetAllPokemonsAsync();

        Task<ResultDto<Pokemon>> GetPokemonByIdAsync(int targetId);

        Task<ResultDto<Pokemon>> CreatePokemonAsync(PokemonDto dto);

        Task<ResultDto<Pokemon>> UpdatePokemonAsync(int targetId, UpdatePokemonDto dto);

        Task<ResultDto<Pokemon>> DeletePokemonAsync(int targetId);

        //PokemonCenter management
        Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(PokemonCenterDto dto);

        Task<ResultDto<PokemonCenter>> UpdatePokemonCenterMarketPriceAsync(int targetId, UpdatePriceDto dto);

        Task<ResultDto<PokemonCenter>> DeletePokemonCenterAsync(int targetId);

        //Transaction management
        Task<ResultDto<IEnumerable<Transaction>>> GetAllTransactionsAsync();

        Task<ResultDto<Transaction>> GetTransactionByIdAsync(int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByUserIdAsync(int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByPokemonIdAsync(int targetId);

        Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsHistoryAsync(int? year = null, int? month = null, int? day = null);

        Task<ResultDto<Transaction>> DeleteTransactionAsync(int targetId);
    }
}