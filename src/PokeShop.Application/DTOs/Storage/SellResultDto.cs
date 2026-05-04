namespace PokeShop.Application.DTOs.Storage
{
    public record class SellResultDto(string UserName, string PokemonName,
        string Nature, IReadOnlyList<Elements> Elements,
        Rarities Rarity, int MarketValue, string CoinsAdjustment);
}