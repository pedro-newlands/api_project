using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class BuyResultDto
    {
        public int ? OwnerId { get; set; }

        public string PokemonName{ get; set; }

        public string Nature { get; set; }

<<<<<<< HEAD
        public string Type { get; set; }
  
        public PokemonRarity Rarity { get; set; }

        public int PokemonMarketValue { get; set; }
=======
        public List<Elements> Elements { get; set; }
  
        public Rarities Rarity { get; set; }

        public int MarketValue { get; set; }
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

        public string CoinsAdjustment { get; set; }
    }
}