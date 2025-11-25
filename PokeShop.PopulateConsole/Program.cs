using System.Text;
using ProjetoPokeShop.Models;
class PopulatePokeShopScript
{
    static void Main()
    {
        string[] names = new string[] { "Pikachu", "Caterpie", "Emolga", "Sentret", "Mareep", "Magikarp", "Ponyta", "Vulpix", "Riolu", "Dratini", "Dragonite", "Bisharp", "Charizard", "Mewtwo", "Rayquaza", "Kyogre", "Groudon", "Articuno", "Mew", "Kartana", "Arceus" };

        string[] natures = new string[] { "Modest", "Adamant", "Timid", "Quiet", "Bashful", "Lonely", "Hard", "Brave", "Docile", "Lax", "Serious", "Gentle", "Calm", "Bold", "Timid", "Adamant", "Quiet", "Modest", "Lax", "Adamant", "Brave" };

        string[] types = new string[] { "Electric", "Bug", "Electric-Flying", "Normal", "Electric", "Water", "Fire", "Fire", "Fighting", "Dragon", "Dragon-Flying", "Dark-Steel", "Fire-Flying", "Psychic", "Dragon-Flying", "Water", "Ground", "Ice-Flying", "Psychic", "Grass-Steel", "Normal" };

        PokemonRarity[] rarities = { PokemonRarity.Common, PokemonRarity.Common, PokemonRarity.Common, PokemonRarity.Common, PokemonRarity.Common, PokemonRarity.Common, PokemonRarity.Uncommon, PokemonRarity.Uncommon, PokemonRarity.Uncommon, PokemonRarity.Uncommon, PokemonRarity.Rare, PokemonRarity.Rare, PokemonRarity.Rare, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary, PokemonRarity.Legendary };

        var rarityValues = new Dictionary<PokemonRarity, int>
        {
            { PokemonRarity.Common, 20 },
            { PokemonRarity.Uncommon, 40 },
            { PokemonRarity.Rare, 60 },
            { PokemonRarity.Legendary, 80}
        };

        List<int> pokemonIds = new List<int>();
        // Dictionary<string, int> rarityValues = new () { };
        var utf8NoBom = new UTF8Encoding(false);
        // using StreamWriter sw = new StreamWriter("PopulatePokeShop.sql"); //versão morder * C8.0 +
        using (StreamWriter sw = new StreamWriter("PopulatePokeShop.sql", false, utf8NoBom)) //versão clássica
        {
            sw.WriteLine("USE PokeShopDb;");
            
            sw.WriteLine("-- Users");
            sw.WriteLine("INSERT INTO Users (UserName, PasswordHash) VALUES ('admin', '1010');");
            sw.WriteLine("INSERT INTO Users (UserName, PasswordHash, Coins, FirstLogin) VALUES ('Ash','1234',100,1),('Misty','1234',100,1),('Brock','1234',100,1);");
            sw.WriteLine();

            sw.WriteLine("-- Pokémons");
            int pokemonIdCounter = 1;
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                string nature = natures[i];
                string type = types[i];
                PokemonRarity rarity = rarities[i];
                int value = rarityValues[rarity];

                sw.WriteLine($"INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('{name}', '{nature}', '{type}', {value}, '{rarity}', NULL);");

                pokemonIds.Add(pokemonIdCounter);
                pokemonIdCounter++;
            }

            sw.WriteLine();

            sw.WriteLine("-- Pokémon Center");
            foreach (var id in pokemonIds)
                sw.WriteLine($"INSERT INTO PokemonCenter (PokemonId) VALUES ({id});");

            sw.WriteLine();
            sw.WriteLine("-- Script generated");
            Console.WriteLine("Script PopulatePokeShop.sql created");
        }

    }
}
