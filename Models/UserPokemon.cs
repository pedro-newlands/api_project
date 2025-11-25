using System.Data;

namespace ProjetoPokeShop.Models
{
    public class UserPokemon
    {
        public int Id { get; set; } // PK

        public int UserId { get; set; } // FK

        public User User { get; set; }

        public DateTime AcquiredAt { get; set; } = DateTime.Now;

        public int PokemonId { get; set; } // FK
        public Pokemon Pokemon { get; set; }

    }
}