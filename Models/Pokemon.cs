using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Models
{
    public class Pokemon
    {
        public int Id { get; set; } // PK

        public string Name { get; set; }

        public string Nature { get; set; } // Para possível lógica futura de maior raridade por natureza

        public ICollection<Element> Elements { get; set; } = new List<Element>(); // Para possível filter futuro por tipo

        public int RarityId { get; set; } 

        public Rarity Rarity {get; set; } // Common, Uncommon, Rare, Legendary

        public int? OwnerId { get; set; } // NULL = disponível

        public User? Owner { get; set; }
    }
}