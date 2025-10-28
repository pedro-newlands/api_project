namespace ProjetoPokeShop.Models
{
    public class PokemonCenter
    {
        public int Id { get; set; } // PK

        public int PokemonId { get; set; } //FK

        public Pokemon Pokemon { get; set;  }
    }
}