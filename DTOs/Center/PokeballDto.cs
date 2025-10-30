using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokeballDto
    {
        public string Pokeball { get; set; }
        
        public PokemonRarity Rarity { get; set; }

        public string PokemonName { get; set; }

        public string Nature { get; set; }

        public string Type { get; set; }

        public int PokemonMarketValue { get; set; }

        public int ? OwnerId { get; set; }

        public string CoinsAdjustment { get; set; }
    }
}