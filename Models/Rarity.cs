using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Models
{
    public class Rarity
    {
        public int Id {get; set; }

        public Rarities Name {get; set; }

        public int Price {get; set; }

        public ICollection<Pokemon> Pokemons {get; set; } = new List<Pokemon>();
    }
}