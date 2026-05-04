namespace PokeShop.Application.DTOs.Center
{
    public record class PokeballDto(string Pokeball, Rarities Rarity, 
        string PokemonName, string Nature, 
        IReadOnlyList<Elements> Elements, int MarketValue,
        int ? OwnerId, string CoinsAdjustment);
}