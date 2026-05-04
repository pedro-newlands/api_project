namespace PokeShop.Application.DTOs.Storage
{
    public record class EngagedPokemonDto(int UserPokemonId, string Name, 
        string Nature, IReadOnlyList<Elements> Elements, 
        int MarketValue, Rarities Rarity);
}