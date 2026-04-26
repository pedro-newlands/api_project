using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Models
{
    public class Element
    {
        public int Id {get; set; }

        public Elements Name {get; set; }

        public ICollection<Pokemon> Pokemons {get; set; } = new List<Pokemon>();
    }
}