using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokemonDto
    {
        public string Name { get; set; } = string.Empty;
        public string Nature { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public PokemonRarity Rarity { get; set; }
        public int Value { get; set; }
        public int OwnerId { get; set; }
    }
}