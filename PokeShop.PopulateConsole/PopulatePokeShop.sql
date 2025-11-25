USE PokeShopDb;
-- Users
INSERT INTO Users (UserName, PasswordHash) VALUES ('admin', '1010');
INSERT INTO Users (UserName, PasswordHash, Coins, FirstLogin) VALUES ('Ash','1234',100,1),('Misty','1234',100,1),('Brock','1234',100,1);

-- Pokémons
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Pikachu', 'Modest', 'Electric', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Caterpie', 'Adamant', 'Bug', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Emolga', 'Timid', 'Electric-Flying', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Sentret', 'Quiet', 'Normal', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Mareep', 'Bashful', 'Electric', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Magikarp', 'Lonely', 'Water', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Ponyta', 'Hard', 'Fire', 40, 'Uncommon', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Vulpix', 'Brave', 'Fire', 40, 'Uncommon', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Riolu', 'Docile', 'Fighting', 40, 'Uncommon', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Dratini', 'Lax', 'Dragon', 40, 'Uncommon', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Dragonite', 'Serious', 'Dragon-Flying', 60, 'Rare', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Bisharp', 'Gentle', 'Dark-Steel', 60, 'Rare', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Charizard', 'Calm', 'Fire-Flying', 60, 'Rare', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Mewtwo', 'Bold', 'Psychic', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Rayquaza', 'Timid', 'Dragon-Flying', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Kyogre', 'Adamant', 'Water', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Groudon', 'Quiet', 'Ground', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Articuno', 'Modest', 'Ice-Flying', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Mew', 'Lax', 'Psychic', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Kartana', 'Adamant', 'Grass-Steel', 80, 'Legendary', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Arceus', 'Brave', 'Normal', 80, 'Legendary', NULL);

-- Pokémon Center
INSERT INTO PokemonCenter (PokemonId) VALUES (1);
INSERT INTO PokemonCenter (PokemonId) VALUES (2);
INSERT INTO PokemonCenter (PokemonId) VALUES (3);
INSERT INTO PokemonCenter (PokemonId) VALUES (4);
INSERT INTO PokemonCenter (PokemonId) VALUES (5);
INSERT INTO PokemonCenter (PokemonId) VALUES (6);
INSERT INTO PokemonCenter (PokemonId) VALUES (7);
INSERT INTO PokemonCenter (PokemonId) VALUES (8);
INSERT INTO PokemonCenter (PokemonId) VALUES (9);
INSERT INTO PokemonCenter (PokemonId) VALUES (10);
INSERT INTO PokemonCenter (PokemonId) VALUES (11);
INSERT INTO PokemonCenter (PokemonId) VALUES (12);
INSERT INTO PokemonCenter (PokemonId) VALUES (13);
INSERT INTO PokemonCenter (PokemonId) VALUES (14);
INSERT INTO PokemonCenter (PokemonId) VALUES (15);
INSERT INTO PokemonCenter (PokemonId) VALUES (16);
INSERT INTO PokemonCenter (PokemonId) VALUES (17);
INSERT INTO PokemonCenter (PokemonId) VALUES (18);
INSERT INTO PokemonCenter (PokemonId) VALUES (19);
INSERT INTO PokemonCenter (PokemonId) VALUES (20);
INSERT INTO PokemonCenter (PokemonId) VALUES (21);

-- Script generated
