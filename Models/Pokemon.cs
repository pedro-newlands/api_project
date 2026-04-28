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
<<<<<<< HEAD
        public User? Owner { get; set; }
        
        public static int GetDefaultValue(PokemonRarity rarity) => rarity switch
        {
            PokemonRarity.Common => 20,
            PokemonRarity.Uncommon => 40,
            PokemonRarity.Rare => 60,
            PokemonRarity.Legendary => 80,
            _ => 0
        };
=======

        public User? Owner { get; set; }
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    }
}