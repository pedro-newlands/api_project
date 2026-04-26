USE PokeShopDb;
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE UserPokemons; TRUNCATE TABLE PokemonCenter; TRUNCATE TABLE PokemonElement; TRUNCATE TABLE Pokemons; TRUNCATE TABLE Elements; TRUNCATE TABLE Rarities; TRUNCATE TABLE Users;
SET FOREIGN_KEY_CHECKS = 1;

-- Rarities
INSERT INTO Rarities (Id, Name, Price) VALUES (1, 'Common', 20), (2, 'Uncommon', 40), (3, 'Rare', 60), (4, 'Legendary', 80);

-- Elements
INSERT INTO Elements (Id, Name) VALUES (1, 'Normal');
INSERT INTO Elements (Id, Name) VALUES (2, 'Fire');
INSERT INTO Elements (Id, Name) VALUES (3, 'Water');
INSERT INTO Elements (Id, Name) VALUES (4, 'Electric');
INSERT INTO Elements (Id, Name) VALUES (5, 'Grass');
INSERT INTO Elements (Id, Name) VALUES (6, 'Ice');
INSERT INTO Elements (Id, Name) VALUES (7, 'Fighting');
INSERT INTO Elements (Id, Name) VALUES (8, 'Poison');
INSERT INTO Elements (Id, Name) VALUES (9, 'Ground');
INSERT INTO Elements (Id, Name) VALUES (10, 'Flying');
INSERT INTO Elements (Id, Name) VALUES (11, 'Psychic');
INSERT INTO Elements (Id, Name) VALUES (12, 'Bug');
INSERT INTO Elements (Id, Name) VALUES (13, 'Rock');
INSERT INTO Elements (Id, Name) VALUES (14, 'Ghost');
INSERT INTO Elements (Id, Name) VALUES (15, 'Dragon');
INSERT INTO Elements (Id, Name) VALUES (16, 'Dark');
INSERT INTO Elements (Id, Name) VALUES (17, 'Steel');
INSERT INTO Elements (Id, Name) VALUES (18, 'Fairy');

-- Users
INSERT INTO Users (Id, UserName, PasswordHash, Coins, FirstLogin) VALUES (1, 'admin','1010', 0, 0);

-- Pokémons e Relações
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Pikachu', 'Modest', 1, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (4, 1);
INSERT INTO PokemonCenter (PokemonId) VALUES (1);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Caterpie', 'Adamant', 1, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (12, 2);
INSERT INTO PokemonCenter (PokemonId) VALUES (2);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Charizard', 'Timid', 3, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (2, 3);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (10, 3);
INSERT INTO PokemonCenter (PokemonId) VALUES (3);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Rayquaza', 'Jolly', 4, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (15, 4);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (10, 4);
INSERT INTO PokemonCenter (PokemonId) VALUES (4);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Bisharp', 'Serious', 3, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (16, 5);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (17, 5);
INSERT INTO PokemonCenter (PokemonId) VALUES (5);
