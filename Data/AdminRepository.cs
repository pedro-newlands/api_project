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
    
        //user
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

        public async Task<User> UpdateUserAsync(User user, UpdateUserDto dto)
        {
            user.UserName = dto.UpUsername ?? user.UserName;
            user.Coins = dto.UpCoins ?? user.Coins;
            user.FirstLogin = dto.FirstLogin ?? user.FirstLogin;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //pokemon
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
                // .AsNoTracking()  Melhora a performance, pois o EF não precisa monitorar mudanças nesse objeto
                .Include(p => p.Owner)
                .Include(p => p.Elements)
                .Include(p => p.Rarity)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pokemon?> CreatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            var createdPokemon = await GetPokemonByIdAsync(pokemon.Id);
            return createdPokemon;
        }

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
        }

        public async Task DeletePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

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
            return await _context.PokemonCenter
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Elements)
                .Include(pc => pc.Pokemon)
                    .ThenInclude(p => p.Rarity)
                .FirstOrDefaultAsync(pc => pc.Id == targetId);
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

        public async Task DeletePokemonCenterAsync(PokemonCenter pokemonCenter)
        {
            _context.PokemonCenter.Remove(pokemonCenter);
            await _context.SaveChangesAsync();
        }
    }
}