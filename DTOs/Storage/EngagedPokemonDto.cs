using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class EngagedPokemonDto
    {
        public int UserPokemonId { get; set; }

        public string Name { get; set; }

        public string Nature { get; set; }

        public string Type { get; set; }

        public int MarketValue { get; set; }

        public PokemonRarity Rarity { get; set; }

        public DateTime AcquiredAt { get; set; }
    }
}