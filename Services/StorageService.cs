using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Repositories;

namespace ProjetoPokeShop.Services
{
    public class StorageService : IStorageService
    {
        readonly StorageRepository _repository;

        public StorageService(StorageRepository repository)
        {
            _repository = repository;
        }

<<<<<<< HEAD
    public async Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId)
    {
        if (!await _repository.UserExistsByIdasync(userId))
            throw new KeyNotFoundException("User does not exist");

        if (!await _repository.UserPokemonExistsByUserIdAsync(userId))
            throw new KeyNotFoundException("No inventory for this user");

        var storage = await _repository.GetUserInventoryAsListAsync(userId);

        var result = new List<EngagedPokemonDto>();

        foreach (var i in storage)
        {
            result.Add(new EngagedPokemonDto
            {
                UserPokemonId = i.Id,
                Name = i.Pokemon.Name,
                Nature = i.Pokemon.Nature,
                Type = i.Pokemon.Type,
                MarketValue = i.Pokemon.Value,
                Rarity = i.Pokemon.Rarity,
                AcquiredAt = i.AcquiredAt
            });
        }

        return result;
    }
                

        public async Task<SellResultDto> SellPokemonAsync(int userId, int userPokemonId)
        {

            var userPokemon = await _repository.GetUserPokemonById(userPokemonId);

            if (userPokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            if (userPokemon.UserId != userId)
=======
        public async Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId)
        {
            if (!await _repository.UserExistsByIdAsync(userId))
                throw new KeyNotFoundException("User does not exist");

            if (!await _repository.UserOwnsSomeInventary(userId))
                throw new KeyNotFoundException("No inventory for this user");

            var storage = await _repository.GetUserInventoryAsListAsync(userId);

            if (!storage.Any())
                return Enumerable.Empty<EngagedPokemonDto>();

            return storage.Select(p => new EngagedPokemonDto
            {
                UserPokemonId = p.Id,
                Name = p.Name,
                Nature = p.Nature,
                Elements = p.Elements.Select(e => e.Name).ToList(),
                MarketValue = p.Rarity.Price,
                Rarity = p.Rarity.Name,
            });
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int userId)
        {   
            if (!await _repository.UserExistsByIdAsync(userId))
                throw new KeyNotFoundException("User does not exist");

            return await _repository.GetTransactionsAsListAsync(userId);
        }
                
        public async Task<SellResultDto> SellPokemonAsync(int userId, int pokemonId)
        {

            var pokemon = await _repository.GetPokemonById(pokemonId);

            if (pokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            if (pokemon.OwnerId != userId)
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                throw new InvalidOperationException("This pokémon does not belong to this user");

            var user = await _repository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User does not exist");

<<<<<<< HEAD
            int userPokemonValue = userPokemon.Pokemon.Value;

            await _repository.SellUserPokemonAsync(userPokemon, user);
=======
            int pokemonPrice = pokemon.Rarity.Price;

            var existing = await _repository.GetPokemonCenterByIdAsync(pokemonId);
            if (existing != null)
            {
                existing.MarketPrice =  pokemonPrice;
            } else
            {
                await _repository.ReturnToPokemonCenter(pokemon);
            }

            await _repository.SellPokemonAsync(user, pokemon);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

            return new SellResultDto
            {
                UserName = user.UserName,

<<<<<<< HEAD
                PokemonName = userPokemon.Pokemon.Name,

                Nature = userPokemon.Pokemon.Nature,

                Type = userPokemon.Pokemon.Type,

                Rarity = userPokemon.Pokemon.Rarity,

                PokemonMarketValue = userPokemonValue,

                CoinsAdjustment = $"+ {userPokemonValue:C0}"
=======
                PokemonName = pokemon.Name,

                Nature = pokemon.Nature,

                Elements = pokemon.Elements.Select(e => e.Name).ToList(),

                Rarity = pokemon.Rarity.Name,

                MarketValue = pokemonPrice,

                CoinsAdjustment = $"+ {pokemonPrice:C0}"
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            };
        }
    }
}
