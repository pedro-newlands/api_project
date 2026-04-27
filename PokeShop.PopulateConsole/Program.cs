﻿using System.Text;
using ProjetoPokeShop.Models;

class PopulatePokeShopScript
{
    static void Main()
    {
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