using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Repositories
{
    public class CenterRepository
    {
        private readonly AppDbContext _context;

        public CenterRepository(AppDbContext context) => _context = context;

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemonsAsync()
        {
            return await _context.PokemonCenter
<<<<<<< HEAD
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
=======
                .Select(pc => new AvailablePokemonDto
                {
                    PokemonCenterId = pc.PokemonId,
                    Name = pc.Pokemon.Name,
                    Nature = pc.Pokemon.Nature,
                    Elements = pc.Pokemon.Elements.Select(e => e.Name).ToList(),
                    MarketValue = pc.MarketPrice,
                    Rarity = pc.Pokemon.Rarity.Name
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                })
                .AsNoTracking()
                .ToListAsync();
        }

<<<<<<< HEAD
        public async Task<List<Pokemon>> GetAvailablePokemonsByRarityAsync(PokemonRarity rarity)
        {
            return await _context.Pokemons
                .Where(p => p.Rarity == rarity && p.OwnerId == null)
                .ToListAsync();
        }

        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int pokemonCenterId)
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                .FirstOrDefaultAsync(pc => pc.Id == pokemonCenterId);
=======
        public async Task<List<Pokemon>> GetAvailablePokemonsByRarityAsync(Rarities rarity)
        {
            return await _context.Pokemons
                .Include(p => p.Rarity)     
                .Include(p => p.Elements) 
                .Where(p => p.Rarity.Name == rarity && p.OwnerId == null)
                .ToListAsync();
        }

        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id)
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Rarity)
                .FirstOrDefaultAsync(pc => pc.PokemonId == id);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        }

        public async Task BuyPokemonAsync(PokemonCenter pokemonCenter, User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

<<<<<<< HEAD
            user.Coins -= pokemonCenter.Pokemon.Value;

            pokemonCenter.Pokemon.OwnerId = user.Id;

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = user.Id,
                PokemonId = pokemonCenter.PokemonId
            });

=======
            var price = pokemonCenter.MarketPrice;

            user.Coins -= price;

            pokemonCenter.Pokemon.OwnerId = user.Id;

            _context.Transactions.Add(new Transaction
            {
                UserId = user.Id,
                PokemonId = pokemonCenter.PokemonId,
                Status = TransactionStatus.Owned, // 
                CoinsAdjustment = $"- {price:C0}",  // 
            });

            _context.PokemonCenter.Remove(pokemonCenter);

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task BuyPokeballAsync(User user, Pokemon randomPokemon)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            const int pokeballCost = 45;

            user.Coins -= pokeballCost;

<<<<<<< HEAD
            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = user.Id,
                PokemonId = randomPokemon.Id
=======
            _context.Transactions.Add(new Transaction
            {
                UserId = user.Id,
                PokemonId = randomPokemon.Id,
                Status = TransactionStatus.Owned,
                CoinsAdjustment = $"- {pokeballCost}",
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            });

            randomPokemon.OwnerId = user.Id;

<<<<<<< HEAD
=======

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}