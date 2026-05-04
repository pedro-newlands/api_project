using System.Linq.Expressions;

namespace PokeShop.Infra.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context) => _context = context;
    
        //users
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UserExistsByIdAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserExistsByNameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _context.SaveChangesAsync();
            return user;
        }

        //pokemons
        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            return await _context.Pokemons
                .Include(p => p.Elements)
                .Include(p => p.Rarity)
                .Include(p => p.Owner)
                .ToListAsync();
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            return await _context.Pokemons
                .Include(p => p.Owner)
                .Include(p => p.Elements)
                .Include(p => p.Rarity)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Element>> GetElementsByNames(List<Elements> elementsNames)
        {
            var elements = await _context.Elements
                .Where(e => elementsNames.Contains(e.Name))
                .ToListAsync();
            
            return elements;
        }

        public async Task<bool> PokemonExistsByIdAsync(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            var createdPokemon = await GetPokemonByIdAsync(pokemon.Id);
            return createdPokemon;
        }


        public async Task<Pokemon> UpdatePokemonAsync(Pokemon pokemon)
        {
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task DeletePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

        //pokemonCenter
        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int id)
        {
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                .FirstOrDefaultAsync(pc => pc.PokemonId == id);
        }

        public async Task<bool> PokemonCenterExistsById(int id)
        {
            return await _context.PokemonCenter.AnyAsync(pc => pc.PokemonId == id);
        }

        public async Task<PokemonCenter> CreatePokemonCenterAsync(PokemonCenter pokemonCenter)
        {
            
            _context.PokemonCenter.Add(pokemonCenter);

            await _context.SaveChangesAsync();
            
            var createdPokemonCenter = await GetPokemonCenterByIdAsync(pokemonCenter.PokemonId);
            return createdPokemonCenter;
        }

        public async Task<PokemonCenter> UpdatePokemonCenterAsync(PokemonCenter pokemonCenter)
        { 
            await _context.SaveChangesAsync();
            return pokemonCenter;
        }

        public async Task DeletePokemonCenterAsync(PokemonCenter pokemonCenter)
        {
            _context.PokemonCenter.Remove(pokemonCenter);
            await _context.SaveChangesAsync();
        }

        //transactions
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Pokemon)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsHistoryAsync(Expression<Func<Transaction, bool>> filter)
        {
            return await _context.Transactions
                .Where(filter)
                .Include(t => t.Pokemon)
                .Include(t => t.User)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Pokemon)
                .FirstOrDefaultAsync(t => t.PokemonId == id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int id)
        {
            return await _context.Transactions
                .Where(t => t.UserId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByPokemonIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Pokemon)
                    .ThenInclude(p => p.Rarity)
                .Include(t => t.Pokemon)
                    .ThenInclude(p => p.Elements)
                .Where(t => t.PokemonId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

    }
}