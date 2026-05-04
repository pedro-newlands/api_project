namespace PokeShop.Application.DTOs.Management
{
    public record class UserManagementResponseDto(int Id, string UserName,
        int Coins, bool FirstLogin, bool IsActive);
}