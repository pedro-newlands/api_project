using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.DTOs.Entity;
using System.Runtime.CompilerServices;

namespace ProjetoPokeShop.Repositories
{
    public class AdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context) => _context = context;
    
<<<<<<< HEAD
        //user
=======
        //users
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
<<<<<<< HEAD
            return await _context.Users.FindAsync(id);
=======
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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

        public async Task<User> UpdateUserAsync(User user, UpdateUserDto dto)
        {
            user.UserName = dto.UpUsername ?? user.UserName;
            user.Coins = dto.UpCoins ?? user.Coins;
            user.FirstLogin = dto.FirstLogin ?? user.FirstLogin;
<<<<<<< HEAD
=======
            user.IsActive = dto.IsActive ?? user.IsActive;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

            await _context.SaveChangesAsync();
            return user;
        }

<<<<<<< HEAD
        public async Task<User> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //pokemon
        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            return await _context.Pokemons.Include(p => p.Owner).ToListAsync();
=======
        //pokemons
        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            return await _context.Pokemons
                .Include(p => p.Elements)
                .Include(p => p.Rarity)
                .Include(p => p.Owner)
                .ToListAsync();
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
<<<<<<< HEAD
            return await _context.Pokemons.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);
=======
            return await _context.Pokemons
                // AsNoTracking()  Melhora a performance, pois o EF não precisa monitorar mudanças nesse objeto
                .Include(p => p.Owner)
                .Include(p => p.Elements)
                .Include(p => p.Rarity)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> PokemonExistsByIdAsync(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        }

        public async Task<Pokemon?> CreatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            var createdPokemon = await GetPokemonByIdAsync(pokemon.Id);
            return createdPokemon;
        }

<<<<<<< HEAD
        public async Task<Pokemon?> UpdatePokemonAsync(Pokemon pokemon, UpdatePokemonDto dto)
        {
            pokemon.Name = dto.Name ?? pokemon.Name;
            pokemon.Type = dto.Type ?? pokemon.Type;
            pokemon.Nature = dto.Nature ?? pokemon.Nature;
            pokemon.Rarity = dto.Rarity ?? pokemon.Rarity;
            pokemon.Value = dto.Value ?? pokemon.Value;
            pokemon.OwnerId = dto.OwnerId;

            await _context.SaveChangesAsync();
            var updatedPokemon = await GetPokemonByIdAsync(pokemon.Id);
            return updatedPokemon;
=======
        public async Task<List<Element>> GetElementsByNames(List<Elements> elementsNames)
        {
            var elements = await _context.Elements
                .Where(e => elementsNames.Contains(e.Name))
                .ToListAsync();
            
            return elements;
        }

        public async Task<Pokemon?> UpdatePokemonAsync(Pokemon pokemon, UpdatePokemonDto dto)
        {
            pokemon.Name = dto.Name ?? pokemon.Name;
            pokemon.Nature = dto.Nature ?? pokemon.Nature;
            pokemon.RarityId = dto.RarityId ?? pokemon.RarityId;
            pokemon.OwnerId = dto.OwnerId;

            if (dto.Elements != null)
            {
                var newElements = await GetElementsByNames(dto.Elements);
                
                pokemon.Elements.Clear(); 
                foreach (var element in newElements)
                {
                    pokemon.Elements.Add(element);
                }
            }

            await _context.SaveChangesAsync();
            
            return await GetPokemonByIdAsync(pokemon.Id);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        }

        public async Task DeletePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

<<<<<<< HEAD
       //userPokemon 
        public async Task<UserPokemon?> GetUserPokemonByPokemonIdAsync(int pokemonId)
        {
            return await _context.UserPokemons.FirstOrDefaultAsync(up => up.PokemonId == pokemonId);
        }

        public async Task CreateUserPokemonAsync(UserPokemon userPokemon)
        {
            _context.UserPokemons.Add(userPokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserPokemonAsync(UserPokemon userPokemon)
        {
            _context.UserPokemons.Remove(userPokemon);
            await _context.SaveChangesAsync();
        }

        //pokemonCenter
        public async Task<PokemonCenter?> GetPokemonCenterByIdAsync(int targetId)
        {
            return await _context.PokemonCenter.Include(pc => pc.Pokemon).FirstOrDefaultAsync(pc => pc.Id == targetId);
        }

        public async Task<bool> PokemonCenterExistsByPokemonId(int pokemonId)
        {
            return await _context.PokemonCenter.AnyAsync(pc => pc.PokemonId == pokemonId);
        }

        public async Task<PokemonCenter?> CreatePokemonCenterAsync(PokemonCenter center)
        {
            _context.PokemonCenter.Add(center);
            await _context.SaveChangesAsync();
            var createdPokemonCenter = await GetPokemonCenterByIdAsync(center.Id);
            return createdPokemonCenter;
        }

=======
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

        public async Task<PokemonCenter?> CreatePokemonCenterAsync(PokemonCenter pokemonCenter)
        {
            
            _context.PokemonCenter.Add(pokemonCenter);

            await _context.SaveChangesAsync();
            
            var createdPokemonCenter = await GetPokemonCenterByIdAsync(pokemonCenter.PokemonId);
            return createdPokemonCenter;
        }

        public async Task<PokemonCenter?> UpdatePokemonCenterMarketPriceAsync(PokemonCenter pokemonCenter, int? marketPrice)
        { 
            pokemonCenter.MarketPrice = marketPrice.Value;

            await _context.SaveChangesAsync();
            return await GetPokemonCenterByIdAsync(pokemonCenter.PokemonId);
        }

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        public async Task DeletePokemonCenterAsync(PokemonCenter pokemonCenter)
        {
            _context.PokemonCenter.Remove(pokemonCenter);
            await _context.SaveChangesAsync();
        }
<<<<<<< HEAD
=======

        //transactions
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Pokemon)
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

        public IQueryable<Transaction> GetTransactionsHistoryAsync()
        {
            return _context.Transactions
                .Include(t => t.Pokemon)
                .Include(t => t.User)
                .AsNoTracking();
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    }
}