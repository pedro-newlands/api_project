using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<PokemonDto> Pokeball(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            const int pokeballCost = 20;

            if (user.Coins < pokeballCost)
                throw new ArgumentException("Not enough coins");

            user.Coins -= pokeballCost;

            var rarity = RollRarity();

            var pokemons = await _context.Pokemons
                .Where(p => p.Rarity == rarity)
                .ToListAsync();

            if (!pokemons.Any())
                throw new InvalidOperationException("None Pokemon from this rarity exists");

            var random = new Random();
            var randomPokemon = pokemons[random.Next(pokemons.Count)];

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = userId,

                PokemonId = randomPokemon.Id
            });

            await _context.SaveChangesAsync();

            return new PokemonDto
            {
                Name = randomPokemon.Name,

                Nature = randomPokemon.Nature,

                Type = randomPokemon.Type,

                Value = randomPokemon.Value,

                Rarity = rarity
            };
        }
        
        private PokemonRarity RollRarity()
        {
            var random = new Random().Next(100);

            if (random < 45)
                return PokemonRarity.Common;
            else  if (random < 75)
                return PokemonRarity.Uncommon;
            else if (random < 95)
                return PokemonRarity.Rare;
            else 
                return PokemonRarity.Legendary;
        }
    }
}