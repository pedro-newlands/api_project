-- Users
INSERT INTO Users (UserName, PasswordHash, Coins, FirstLogin) VALUES ('Ash','1234',100,1),('Misty','1234',100,1),('Brock','1234',100,1);

-- Pokémons
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Pikachu', 'Modest', 'Electric', 20, 'Common', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Bicharp', 'Adamant', 'Dark-Steel', 40, 'Uncommon', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Charizard', 'Timid', 'Fire-Fly', 60, 'Rare', NULL);
INSERT INTO Pokemons (Name, Nature, Type, Value, Rarity, OwnerId) VALUES ('Mewtwo', 'Quiet', 'Psychic', 80, 'Legendary', NULL);

-- Pokémon Center
INSERT INTO PokemonCenter (PokemonId) VALUES (1);
INSERT INTO PokemonCenter (PokemonId) VALUES (2);
INSERT INTO PokemonCenter (PokemonId) VALUES (3);
INSERT INTO PokemonCenter (PokemonId) VALUES (4);

-- Script generated
