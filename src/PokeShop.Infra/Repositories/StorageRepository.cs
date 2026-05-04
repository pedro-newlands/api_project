namespace PokeShop.Infra.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        readonly AppDbContext _context;

        public StorageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetUserInventoryAsListAsync(int userId)
        {
            return await _context.Pokemons
                .AsNoTracking()
                .Include(p => p.Rarity)
                .Include(p => p.Elements)
                .Where(p => p.OwnerId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsListAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.TransactionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pokemon?> GetPokemonById(int id)
        {
            return await _context.Pokemons
                .Include(p => p.Owner)
                .Include(p => p.Rarity)
                .Include(p => p.Elements)
                .FirstOrDefaultAsync(up => up.Id == id);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UserExistsByIdAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserOwnsSomeInventary(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.OwnerId == id);
        }

        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id)
        {
            return await _context.PokemonCenter  
                .FindAsync(id);
        }

        public async Task SaveSellAsync(Transaction transaction, PokemonCenter? pokemonToReturn = null)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            if (pokemonToReturn != null)
            {
                pokemonToReturn.Pokemon.OwnerId = null;
                _context.PokemonCenter.Add(pokemonToReturn);
            }
            
            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();
        }
    }
}