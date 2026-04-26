using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokeballDto
    {
        public string Pokeball { get; set; }
        
        public Rarities Rarity { get; set; }

        public string PokemonName { get; set; }

        public string Nature { get; set; }

        public List<Elements> Elements { get; set; }

        public int MarketValue { get; set; }

        public int ? OwnerId { get; set; }

        public string CoinsAdjustment { get; set; }
    }
}