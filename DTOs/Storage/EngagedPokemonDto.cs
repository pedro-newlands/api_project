using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class EngagedPokemonDto
    {
        public int UserPokemonId { get; set; }

        public string Name { get; set; }

        public string Nature { get; set; }

<<<<<<< HEAD
        public string Type { get; set; }

        public int MarketValue { get; set; }

        public PokemonRarity Rarity { get; set; }

        public DateTime AcquiredAt { get; set; }
=======
        public List<Elements> Elements { get; set; }

        public int MarketValue { get; set; }

        public Rarities Rarity { get; set; }

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    }
}