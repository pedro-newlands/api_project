namespace PokeShop.Infra.Repositories
{
    public class CenterRepository : ICenterRepository
    {
        private readonly AppDbContext _context;

        public CenterRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<PokemonCenter>> GetAvailablePokemonsAsync() 
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon).ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon).ThenInclude(p => p.Rarity)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id)
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon).ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon).ThenInclude(p => p.Rarity)
                .Where(pc => pc.Pokemon.OwnerId == null)
                .FirstOrDefaultAsync(pc => pc.PokemonId == id);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        
        public async Task<List<Pokemon>> GetAvailablePokemonsByRarityAsync(Rarities rarity)
        {
            return await _context.Pokemons
                .Include(p => p.Rarity)     
                .Include(p => p.Elements) 
                .Where(p => p.Rarity.Name == rarity && p.OwnerId == null)
                .ToListAsync();
        }

        public async Task SavePurchaseAsync(Transaction transaction, PokemonCenter? centerToRemove = null)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            if (centerToRemove != null)  _context.PokemonCenter.Remove(centerToRemove);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();
        }
    }
}