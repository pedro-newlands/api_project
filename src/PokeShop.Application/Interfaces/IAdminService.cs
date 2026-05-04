using PokeShop.Application.DTOs.Management;

namespace PokeShop.Application.Interfaces
{
    public interface IAdminService
    {
        //User management
        Task<ResultDto<IEnumerable<UserManagementResponseDto>>> GetUsersAsync();
        Task<ResultDto<UserManagementResponseDto>> GetUserByIdAsync(int targetId);
        Task<ResultDto<UserManagementResponseDto>> CreateUserAsync(UserManagementCreateDto dto);
        Task<ResultDto<UserManagementResponseDto>> UpdateUserAsync(int targetId, UserManagementUpdateDto dto);
        Task<ResultDto<UserManagementResponseDto>> DeleteUserAsync(int targetId);

        //Pokemon management
        Task<ResultDto<IEnumerable<PokemonManagementResponseDto>>> GetAllPokemonsAsync();
        Task<ResultDto<PokemonManagementResponseDto>> GetPokemonByIdAsync(int targetId);
        Task<ResultDto<PokemonManagementResponseDto>> CreatePokemonAsync(PokemonManagementCreateDto dto);
        Task<ResultDto<PokemonManagementResponseDto>> UpdatePokemonAsync(int targetId, PokemonManagementUpdateDto dto);
        Task<ResultDto<PokemonManagementResponseDto>> DeletePokemonAsync(int targetId);
        
        //PokemonCenter management
        Task<ResultDto<PokemonCenterManagementResponseDto>> CreatePokemonCenterAsync(PokemonCenterManagementCreateDto dto);
        Task<ResultDto<PokemonCenterManagementResponseDto>> UpdatePokemonCenterMarketPriceAsync(int targetId, PokemonCenterManagementUpdateDto dto);
        Task<ResultDto<PokemonCenterManagementResponseDto>> DeletePokemonCenterAsync(int targetId);
        
        //Transaction management
        Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetAllTransactionsAsync();
        Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsHistoryAsync(int? year = null, int? month = null, int? day = null);
        Task<ResultDto<TransactionManagementResponseDto>> GetTransactionByIdAsync(int targetId);
        Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsByUserIdAsync(int targetId);
        Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsByPokemonIdAsync(int targetId);
    }
}