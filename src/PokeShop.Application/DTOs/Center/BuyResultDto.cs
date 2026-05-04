namespace PokeShop.Application.DTOs.Center
{
    public record class BuyResultDto(int ? OwnerId, string PokemonName, 
        string Nature, IReadOnlyList<Elements> Elements, 
        Rarities Rarity, int MarketValue, string CoinsAdjustment);
}