using ProjetoPokeShop.DTOs;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;

namespace api_project.Services
{
    public class ManagementService : IManagementService
    {
        readonly AppDbContext _context;

        public ManagementService(AppDbContext context) => _context = context;

        //user management
        public async Task<ResultDto<IEnumerable<User>>> GetUsers(string superPassword)
        {
            await AdminAsync(superPassword);

            var users = await _context.Users.ToListAsync();

            return new ResultDto<IEnumerable<User>>
            {
                Message = "'Get' succeed",

                TargetEntity = users
            };
        }

        public async Task<ResultDto<User>> UpdateUser(string superPassword, int targetId, UserDto dto)
        {
            await AdminAsync(superPassword);

            var user = await _context.Users.FindAsync(targetId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.UserName = dto.UpUsername;
            user.Coins = dto.UpCoins;

            await _context.SaveChangesAsync();

            return new ResultDto<User>
            {
                Message = "'Update' succeed",

                TargetEntity = user
            };
        }

        public async Task<ResultDto<User>> DeleteUser(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetUser = await _context.Users.FindAsync(targetId);

            if (targetUser == null)
                throw new KeyNotFoundException("User does not exist");

            _context.Users.Remove(targetUser);
            await _context.SaveChangesAsync();

            return new ResultDto<User>
            {
                Message = "'Delete' succeed",

                TargetEntity = targetUser
            };
        }

        //pokemon management
        public async Task<ResultDto<IEnumerable<Pokemon>>> GetPokemons(string superPassword)
        {
            await AdminAsync(superPassword);

            var pokemons = await _context.Pokemons.ToListAsync();

            return new ResultDto<IEnumerable<Pokemon>>
            {
                Message = "'Get' succeed",

                TargetEntity = pokemons
            };
        }

        public async Task<ResultDto<Pokemon>> GetPokemonById(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var pokemon = await _context.Pokemons.FindAsync(targetId);

            return new ResultDto<Pokemon>
            {
                Message = "'Get' succeed",
                TargetEntity = pokemon
            };
        }

        public async Task<ResultDto<Pokemon>> CreatePokemon(string superPassword, PokemonDto dto)
        {
            await AdminAsync(superPassword);

            Pokemon pokemon = new()
            {
                Name = dto.Name,

                Nature = dto.Nature,

                Type = dto.Type,

                Rarity = dto.Rarity,

                Value = dto.Value,

                OwnerId = dto.OwnerId
            };

            var exists = await _context.Pokemons
                .AnyAsync(p => EF.Functions.Collate(p.Name, "SQL_Latin1_General_CP1_CI_AS") == pokemon.Name);

            if (exists)
                throw new InvalidOperationException("This pokémon already exists");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == pokemon.OwnerId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            var centerEntry = new PokemonCenter
            {
                Pokemon = pokemon
            };

            _context.Pokemons.Add(pokemon);
            _context.PokemonCenter.Add(centerEntry);
            await _context.SaveChangesAsync();

            return new ResultDto<Pokemon>
            {
                Message = "'Post' succeed",

                TargetEntity = pokemon
            };
        }

        public async Task<ResultDto<Pokemon>> UpdatePokemon(string superPassword, int targetId, UpdatePokemonDto dto)
        {
            await AdminAsync(superPassword);

            var pokemon = await _context.Pokemons.FindAsync(targetId);

            if (pokemon == null)
                throw new KeyNotFoundException("Pokemon not Found");

            pokemon.Name = dto.Name ?? pokemon.Name;
            pokemon.Type = dto.Type ?? pokemon.Type;
            pokemon.Nature = dto.Nature ?? pokemon.Nature;
            pokemon.Rarity = dto.Rarity ?? pokemon.Rarity;
            pokemon.Value = dto.Value ?? pokemon.Value;
            pokemon.OwnerId = dto.OwnerId ?? pokemon.OwnerId;

            await _context.SaveChangesAsync();

            return new ResultDto<Pokemon>
            {
                Message = "'Update' succeed",

                TargetEntity = pokemon
            };
        }

        public async Task<ResultDto<Pokemon>> DeletePokemon(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetPokemon = await _context.Pokemons.FindAsync(targetId);

            if (targetPokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            _context.Pokemons.Remove(targetPokemon);
            await _context.SaveChangesAsync();

            return new ResultDto<Pokemon>
            {
                Message = "'Delete' succeed",

                TargetEntity = targetPokemon
            };
        }

        public async Task<ResultDto<PokemonCenter>> DeletePokemonCenter(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetPokemonCenter = await _context.PokemonCenter.FindAsync(targetId);

            if (targetPokemonCenter == null)
                throw new KeyNotFoundException("Pokémon does not exist in the store");

            _context.PokemonCenter.Remove(targetPokemonCenter);
            await _context.SaveChangesAsync();

            return new ResultDto<PokemonCenter>
            {
                Message = "'Delete' succeed",

                TargetEntity = targetPokemonCenter
            };
        }

        public async Task<ResultDto<PokemonCenter>> RestorePokemonCenter(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var pokemonWish = await _context.Pokemons.FindAsync(targetId);

            if (pokemonWish == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            var exists = await _context.PokemonCenter.AnyAsync(pc => pc.PokemonId == targetId);

            if (exists)
                throw new InvalidOperationException("Pokémon is in Center already");

            PokemonCenter pokemonCenter = new()
            {
                PokemonId = targetId
            };
            _context.Add(pokemonCenter);
            await _context.SaveChangesAsync();


            return new ResultDto<PokemonCenter>
            {
                Message = "'Post' succeed",
                TargetEntity = pokemonCenter
            };
        }

        private async Task AdminAsync(string superPassword)
        {
            var admin = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == "admin" && u.PasswordHash == superPassword);

            if (admin == null)
                throw new InvalidOperationException("Not allowed");
        }
    }
}