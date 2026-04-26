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
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Rarity)
                .Where(pc => pc.Pokemon.OwnerId == null)
                .Select(pc => new AvailablePokemonDto
                {
                    PokemonCenterId = pc.Id,
                    Name = pc.Pokemon.Name,
                    Nature = pc.Pokemon.Nature,
                    Elements = pc.Pokemon.Elements.Select(e => e.Name).ToList(),
                    MarketValue = pc.Pokemon.Rarity.Price,
                    Rarity = pc.Pokemon.Rarity.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Pokemon>> GetAvailablePokemonsByRarityAsync(Rarities rarity)
        {
            return await _context.Pokemons
                .Include(p => p.Rarity)     
                .Include(p => p.Elements) 
                .Where(p => p.Rarity.Name == rarity && p.OwnerId == null)
                .ToListAsync();
        }

        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int pokemonCenterId)
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Rarity)
                .FirstOrDefaultAsync(pc => pc.Id == pokemonCenterId);
        }

        public async Task BuyPokemonAsync(PokemonCenter pokemonCenter, User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            user.Coins -= pokemonCenter.Pokemon.Rarity.Price;

            pokemonCenter.Pokemon.OwnerId = user.Id;

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = user.Id,
                PokemonId = pokemonCenter.PokemonId
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task BuyPokeballAsync(User user, Pokemon randomPokemon)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            const int pokeballCost = 45;

            user.Coins -= pokeballCost;

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = user.Id,
                PokemonId = randomPokemon.Id
            });

            randomPokemon.OwnerId = user.Id;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}