using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokemonDto
    {
        public string Name { get; set; }

        public string Nature { get; set; }
        
        public string Type { get; set; }

        public int Value { get; set; }
        
        public PokemonRarity Rarity { get; set; }
    }
}