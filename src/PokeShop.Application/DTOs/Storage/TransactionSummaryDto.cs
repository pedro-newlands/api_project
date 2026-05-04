namespace PokeShop.Application.DTOs.Storage
{
    public record class TransactionSummaryDto(string PokemonName, TransactionStatus Status,
        DateTime TransactionDate, string CoinsAdjustment);
}