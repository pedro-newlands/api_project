using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class EngagedPokemonDto
    {
        public int UserPokemonId { get; set; }

        public string Name { get; set; }

        public string Nature { get; set; }

        public List<Elements> Elements { get; set; }

        public int MarketValue { get; set; }

        public Rarities Rarity { get; set; }

    }
}