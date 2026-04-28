<<<<<<< HEAD
﻿using System.Text;
using ProjetoPokeShop.Models;
=======
﻿﻿using System.Text;
using ProjetoPokeShop.Models;

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
class PopulatePokeShopScript
{
    static void Main()
    {
<<<<<<< HEAD
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
=======
        string[] names = { "Pikachu", "Caterpie", "Charizard", "Rayquaza", "Bisharp" };
        string[] natures = { "Modest", "Adamant", "Timid", "Jolly", "Serious" };
        
        Elements[][] pokemonTypes = {
            new[] { Elements.Electric },
            new[] { Elements.Bug },
            new[] { Elements.Fire, Elements.Flying }, 
            new[] { Elements.Dragon, Elements.Flying },
            new[] { Elements.Dark, Elements.Steel }
        };

        int[] rarityIds = { 1, 1, 3, 4, 3 }; 

        int[] pokemonCenterIds = {1, 2, 3};

        var utf8NoBom = new UTF8Encoding(false);

        using (StreamWriter sw = new StreamWriter("PopulatePokeShop.sql", false, utf8NoBom))
        {
            sw.WriteLine("USE PokeShopDb;");
            sw.WriteLine("SET FOREIGN_KEY_CHECKS = 0;");
            // Usando os nomes exatos da sua Migration para o TRUNCATE
            sw.WriteLine("TRUNCATE TABLE Transactions; TRUNCATE TABLE PokemonCenter; TRUNCATE TABLE PokemonElement; TRUNCATE TABLE Pokemons; TRUNCATE TABLE Elements; TRUNCATE TABLE Rarities; TRUNCATE TABLE Users;");
            sw.WriteLine("SET FOREIGN_KEY_CHECKS = 1;");
            sw.WriteLine();

            // 1. Rarities
            sw.WriteLine("-- Rarities");
            sw.WriteLine("INSERT INTO Rarities (Id, Name, Price) VALUES (1, 'Common', 20), (2, 'Uncommon', 40), (3, 'Rare', 60), (4, 'Legendary', 80);");
            sw.WriteLine();

            // 2. Elements
            sw.WriteLine("-- Elements");
            var allElements = Enum.GetValues<Elements>();
            foreach (var e in allElements)
            {
                sw.WriteLine($"INSERT INTO Elements (Id, Name) VALUES ({(int)e + 1}, '{e}');");
            }
            sw.WriteLine();

            // 3. Users
            sw.WriteLine("-- Users");
            sw.WriteLine("INSERT INTO Users (Id, UserName, PasswordHash, Coins, FirstLogin) VALUES (1, 'admin','1010', 0, 0);");
            sw.WriteLine();

            // 4. Pokemons e Elementos
            sw.WriteLine("-- Pokémons and PokemonElements");
            for (int i = 0; i < names.Length; i++)
            {
                int pId = i + 1;
                
                sw.WriteLine($"INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('{names[i]}', '{natures[i]}', {rarityIds[i]}, NULL);");

                // Vínculos na tabela PokemonElement (conforme sua Migration)
                foreach (var type in pokemonTypes[i])
                {
                    int elementId = (int)type + 1;
                    // Nomes confirmados: ElementsId e PokemonId
                    sw.WriteLine($"INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES ({elementId}, {pId});");
                }
            }
            sw.WriteLine();

            // 5. PokemonCenter
            sw.WriteLine("-- PokemonCenter");
            foreach (var pcId in pokemonCenterIds)
            {
                string sql = $@"INSERT INTO PokemonCenter (PokemonId, MarketPrice) 
                    SELECT p.Id, r.Price 
                    FROM Pokemons p 
                    JOIN Rarities r ON p.RarityId = r.Id 
                    WHERE p.Id = {pcId};";

                sw.WriteLine(sql);
            }

            Console.WriteLine("Arquivo PopulatePokeShop.sql gerado com sucesso baseando-se na Migration!");
        }
    }
}
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
