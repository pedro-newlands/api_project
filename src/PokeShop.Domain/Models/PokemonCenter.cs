namespace PokeShop.Domain.Models
{
    public class PokemonCenter
    {
        public int PokemonId { get; set; } // PK e FK

        public Pokemon Pokemon { get; set;  }

        public int MarketPrice { get; set; }
    }
}