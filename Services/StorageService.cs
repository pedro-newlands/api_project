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
                throw new InvalidOperationException("This pokémon does not belong to this user");

            var user = await _repository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User does not exist");

            int userPokemonValue = userPokemon.Pokemon.Value;

            await _repository.SellUserPokemonAsync(userPokemon, user);

            return new SellResultDto
            {
                UserName = user.UserName,

                PokemonName = userPokemon.Pokemon.Name,

                Nature = userPokemon.Pokemon.Nature,

                Type = userPokemon.Pokemon.Type,

                Rarity = userPokemon.Pokemon.Rarity,

                PokemonMarketValue = userPokemonValue,

                CoinsAdjustment = $"+ {userPokemonValue:C0}"
            };
        }
    }
}
