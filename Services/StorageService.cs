using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public class StorageService : IStorageService
    {
        readonly AppDbContext _context;

        public StorageService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<EngagedPokemonDto>> Inventory(int userId)
        {
            if (await _context.Users.AnyAsync(u => u.Id == userId))
                throw new KeyNotFoundException("User does not exist");

            else if (!await _context.UserPokemons.AnyAsync(up => up.UserId == userId))
                throw new KeyNotFoundException("No inventory for this user");
            
            var storage = await _context.UserPokemons
                .Include(up => up.Pokemon)
                .Where(up => up.UserId == userId)
                .Select(up => new EngagedPokemonDto
                {
                    UserPokemonId = up.Id,

                    Name = up.Pokemon.Name,

                    Nature = up.Pokemon.Nature,

                    Type = up.Pokemon.Type,

                    MarketValue = up.Pokemon.Value,

                    Rarity = up.Pokemon.Rarity,

                    AcquiredAt = DateTime.UtcNow
                })
                .AsNoTracking()
                .ToListAsync();

            return storage;
        }

        public async Task<SellResultDto> SellPokemon(int userId ,int userPokemonId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();


            var userPokemon = await _context.UserPokemons
                .Include(up => up.Pokemon)
                .FirstOrDefaultAsync(up => up.Id == userPokemonId);

            if (userPokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            if (userPokemon.UserId != userId)
                throw new InvalidOperationException("This pokémon does not belong to this user");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new KeyNotFoundException("User does not exist");
            
            int userPokemonValue = userPokemon.Pokemon.Value;

            user.Coins += userPokemonValue;

            userPokemon.Pokemon.OwnerId = null;

            _context.UserPokemons.Remove(userPokemon);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

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
