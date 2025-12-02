using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Repositories
{
    public class StorageRepository
    {
        readonly AppDbContext _context;

        public StorageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPokemon>> GetUserInventoryAsListAsync(int userId)
        {
            return await _context.UserPokemons
                .Include(up => up.Pokemon)
                .Where(up => up.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserPokemon?> GetUserPokemonById(int userPokemonId)
        {
            return await _context.UserPokemons.Include(up => up.Pokemon).FirstOrDefaultAsync(up => up.Id == userPokemonId);
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UserExistsByIdasync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> UserPokemonExistsByUserIdAsync(int userId)
        {
            return await _context.UserPokemons.AnyAsync(up => up.UserId == userId);
        }

        public async Task SellUserPokemonAsync(UserPokemon userPokemon, User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            user.Coins -= userPokemon.Pokemon.Value;
            userPokemon.Pokemon.OwnerId = null;

            _context.UserPokemons.Remove(userPokemon);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}