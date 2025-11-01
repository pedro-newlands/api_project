namespace ProjetoPokeShop.Models
{
    public class Pokemon
    {
        public int Id { get; set; } // PK

        public string Name { get; set; }

        public string Nature { get; set; } // Para possível lógica futura de maior raridade por natureza

        public string Type { get; set; } // Para possível filter futuro por tipo

        public int Value { get; set; }

        public PokemonRarity Rarity { get; set; } // Common, Uncommon, Rare, Legendary

        public int? OwnerId { get; set; } // NULL = disponível
        public User? Owner { get; set; }
        
        public static int GetDefaultValue(PokemonRarity rarity) => rarity switch
        {
            PokemonRarity.Common => 20,
            PokemonRarity.Uncommon => 40,
            PokemonRarity.Rare => 60,
            PokemonRarity.Legendary => 80,
            _ => 0
        };
    }
}