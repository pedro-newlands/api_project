namespace PokeShop.Application.DTOs.Center
{
    public record class AvailablePokemonDto (int PokemonCenterId, string Name, 
        string Nature, IEnumerable<Elements> Elements, int MarketValue, Rarities Rarity);
}