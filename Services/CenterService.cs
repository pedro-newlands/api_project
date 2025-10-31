using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public class CenterService : ICenterService
    {
        readonly AppDbContext _context;
        private static readonly Random _rng = new Random();

        public CenterService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemons()
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                .Where(pc => pc.Pokemon.OwnerId == null)
                .Select(pc => new AvailablePokemonDto
                {
                    PokemonCenterId = pc.Id,
                    
                    Name = pc.Pokemon.Name,

                    Nature = pc.Pokemon.Nature,

                    Type = pc.Pokemon.Type,

                    MarketValue = pc.Pokemon.Value,

                    Rarity = pc.Pokemon.Rarity
                })
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
                throw new KeyNotFoundException("Pokémon not found");

            if (outPokemon.Pokemon.OwnerId != null)
                throw new InvalidOperationException("Pokémon is no longer available");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

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
                OwnerId = outPokemon.Pokemon.OwnerId,

                PokemonName = outPokemon.Pokemon.Name,

                Nature = outPokemon.Pokemon.Nature,

                Type = outPokemon.Pokemon.Type,

                Rarity = outPokemon.Pokemon.Rarity,

                PokemonMarketValue = outPokemonValue,

                CoinsAdjustment = $"- {outPokemonValue:C0}"
            };
        }

        public async Task<PokeballDto> Pokeball(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            const int pokeballCost = 45;

            if (user.Coins < pokeballCost)
                throw new InvalidOperationException("Not enough coins");

            user.Coins -= pokeballCost;

            var rarity = RollRarity();

            var pokemons = await _context.Pokemons
                .Where(p => p.Rarity == rarity && p.OwnerId == null)
                .ToListAsync();

            if (!pokemons.Any())
                throw new InvalidOperationException("No Pokémon available in this rarity");

            var randomPokemon = pokemons[_rng.Next(pokemons.Count)];

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = userId,

                PokemonId = randomPokemon.Id,
            });

            randomPokemon.OwnerId = user.Id;


            await _context.SaveChangesAsync();

            return new PokeballDto
            {
                Pokeball = "Poké Ball",

                Rarity = rarity,

                PokemonName = randomPokemon.Name,

                Nature = randomPokemon.Nature,

                Type = randomPokemon.Type,

                PokemonMarketValue = randomPokemon.Value,

                OwnerId = randomPokemon.OwnerId,

                CoinsAdjustment = $"- {pokeballCost}"
            };
        }

        private PokemonRarity RollRarity()
        {
            var random = _rng.Next(100);

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