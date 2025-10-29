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

        public async Task<List<UserPokemon>> Inventory(int userId)
        {
            if (!await _context.UserPokemons.AnyAsync(up => up.UserId == userId))
                throw new ArgumentException("No inventory for this user");
            
            var storage = await _context.UserPokemons
                .Include(up => up.Pokemon)
                .Where(up => up.UserId == userId)
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
                throw new ArgumentException("Pokémon does not exist");

            if (userPokemon.UserId != userId)
                throw new ArgumentException("This pokémon does not belong to this user");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException("User does not exist");
            
            int userPokemonValue = userPokemon.Pokemon.Value;

            user.Coins += userPokemonValue;

            userPokemon.Pokemon.OwnerId = null;

            _context.UserPokemons.Remove(userPokemon);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new SellResultDto
            {
                UserId = userId,

                PokemonId = userPokemon.Pokemon.Id,

                PokemonMarketValue = userPokemonValue,

                CoinsAdjustment = $"+ {userPokemonValue:C0}"
            };
        }
    }
}
