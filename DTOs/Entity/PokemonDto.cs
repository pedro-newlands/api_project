using System.Text.Json.Serialization;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokemonDto
    {
        public int? PokemonCenterId { get; set; }
        
        public int? UserPokemonId { get; set; }
        
        public string Name { get; set; }

        public string Nature { get; set; }
        
        public string Type { get; set; }

        public int MarketValue { get; set; }

        public PokemonRarity Rarity { get; set; }
    }
}