using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
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

        public async Task<Pokemon> BuyPokemon(int pokemonCenterId, int userId)
        {
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

            user.Coins -= outPokemon.Pokemon.Value;

            outPokemon.Pokemon.OwnerId = userId;

            _context.UserPokemons.Add(new UserPokemon
            {
                UserId = userId,
                PokemonId = outPokemon.PokemonId
            });

            await _context.SaveChangesAsync();
            return outPokemon.Pokemon;
        }
    }
}