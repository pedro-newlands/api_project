using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public class CenterService : ICenterService
    {
        readonly AppDbContext _context;

        public CenterService(AppDbContext context) => _context = context;

        public async Task<List<PokemonCenter>> GetAvailablePokemons()
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                .Where(pc => pc.Pokemon.OwnerId == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BuyResultDto> BuyPokemon(int pokemonCenterId, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();


            var outPokemon = await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                .FirstOrDefaultAsync(pc => pc.Id == pokemonCenterId);

            if (outPokemon == null)
                throw new ArgumentException("Pokémon not found");

            if (outPokemon.Pokemon.OwnerId != null)
                throw new InvalidOperationException("Pokémon is no longer available");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException("User not found");

            if (user.Coins < outPokemon.Pokemon.Value)
                throw new InvalidOperationException("Not enough coins");

            int outPokemonValue = outPokemon.Pokemon.Value;

            user.Coins -= outPokemonValue;

            outPokemon.Pokemon.OwnerId = userId;

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = userId,
                PokemonId = outPokemon.PokemonId
            });

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new BuyResultDto
            {
                Name = outPokemon.Pokemon.Name,

                Nature = outPokemon.Pokemon.Nature,

                Type = outPokemon.Pokemon.Type,

                Value = outPokemon.Pokemon.Value,

                Rarity = outPokemon.Pokemon.Rarity,

                PokemonMarketValue = outPokemonValue,

                CoinsAdjustment = $"- {outPokemonValue}"
            };
        }
    }
}