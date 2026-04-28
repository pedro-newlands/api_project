USE PokeShopDb;
<<<<<<< HEAD
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
=======
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE Transactions; TRUNCATE TABLE PokemonCenter; TRUNCATE TABLE PokemonElement; TRUNCATE TABLE Pokemons; TRUNCATE TABLE Elements; TRUNCATE TABLE Rarities; TRUNCATE TABLE Users;
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

-- Pokémons and PokemonElements
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Pikachu', 'Modest', 1, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (4, 1);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Caterpie', 'Adamant', 1, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (12, 2);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Charizard', 'Timid', 3, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (2, 3);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (10, 3);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Rayquaza', 'Jolly', 4, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (15, 4);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (10, 4);
INSERT INTO Pokemons (Name, Nature, RarityId, OwnerId) VALUES ('Bisharp', 'Serious', 3, NULL);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (16, 5);
INSERT INTO PokemonElement (ElementsId, PokemonId) VALUES (17, 5);

-- PokemonCenter
INSERT INTO PokemonCenter (PokemonId, MarketPrice) 
                    SELECT p.Id, r.Price 
                    FROM Pokemons p 
                    JOIN Rarities r ON p.RarityId = r.Id 
                    WHERE p.Id = 1;
INSERT INTO PokemonCenter (PokemonId, MarketPrice) 
                    SELECT p.Id, r.Price 
                    FROM Pokemons p 
                    JOIN Rarities r ON p.RarityId = r.Id 
                    WHERE p.Id = 2;
INSERT INTO PokemonCenter (PokemonId, MarketPrice) 
                    SELECT p.Id, r.Price 
                    FROM Pokemons p 
                    JOIN Rarities r ON p.RarityId = r.Id 
                    WHERE p.Id = 3;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
