namespace PokeShop.Application.DTOs.Management
{
    public record class TransactionManagementResponseDto(int Id, int UserId,
        int? PokemonId, DateTime TransactionDate, TransactionStatus status);
}