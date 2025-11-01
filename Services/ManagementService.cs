using ProjetoPokeShop.DTOs;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjetoPokeShop.DTOs.Entity;

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

        public async Task<ResultDto<User>> GetUserById(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var user = await _context.Users.FindAsync(targetId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return new ResultDto<User>
            {
                Message = "'Get' succeed",
                TargetEntity = user
            };
        }

        public async Task<ResultDto<User>> CreateUser(string superPassword, UserDto dto)
        {
            await AdminAsync(superPassword);

            var exists = await _context.Users.AnyAsync(u => u.UserName == dto.UserName);

            if (exists)
                throw new InvalidOperationException("User with this name exists already");

            User user = new()
            {
                UserName = dto.UserName,

                PasswordHash = dto.PasswordHash,

                Coins = dto.Coins,

                FirstLogin = dto.FirstLogin
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return new ResultDto<User>
            {
                Message = "'Post' succeed",

                TargetEntity = user
            };
        }

        public async Task<ResultDto<User>> UpdateUser(string superPassword, int targetId, UpdateUserDto dto)
        {
            await AdminAsync(superPassword);

            var Targetuser = await _context.Users.FindAsync(targetId);

            if (Targetuser == null)
                throw new KeyNotFoundException("User not found");

            if (Targetuser.UserName == "admin")
                throw new InvalidOperationException("'admin' can not be updated");

            if (!string.IsNullOrEmpty(dto.UpUsername))
                Targetuser.UserName = dto.UpUsername;

            if (dto.UpCoins.HasValue)
                Targetuser.Coins = dto.UpCoins.Value;

            if (dto.FirstLogin.HasValue)
                Targetuser.FirstLogin = dto.FirstLogin.Value;
           
            await _context.SaveChangesAsync();

            return new ResultDto<User>
            {
                Message = "'Update' succeed",

                TargetEntity = Targetuser
            };
        }

        public async Task<ResultDto<User>> DeleteUser(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetUser = await _context.Users.FindAsync(targetId);

            if (targetUser == null)
                throw new KeyNotFoundException("User does not exist");

            if (targetUser.UserName == "admin")
                throw new InvalidOperationException("'admin' can not be removed");

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

            if (pokemon == null)
                throw new KeyNotFoundException("Pokemon not found");

            return new ResultDto<Pokemon>
            {
                Message = "'Get' succeed",
                TargetEntity = pokemon
            };
        }

        public async Task<ResultDto<Pokemon>> CreatePokemon(string superPassword, PokemonDto dto)
        {
            await AdminAsync(superPassword);


            var exists = dto.OwnerId != null ? await _context.Users.AnyAsync(u => u.Id == dto.OwnerId) : true;
            if (!exists)
                throw new KeyNotFoundException("User not found");

            Pokemon pokemon = new()
            {
                Name = dto.Name,

                Nature = dto.Nature,

                Type = dto.Type,

                Rarity = dto.Rarity,

                Value = dto.Value,

                // OwnerId = dto.OwnerId != 0 ? dto.OwnerId : null

                OwnerId = dto.OwnerId
            };
            
            // var exists = await _context.Pokemons --> MySql
            //     .AnyAsync(p => EF.Functions.Collate(p.Name, "SQL_Latin1_General_CP1_CI_AS") == pokemon.Name);

            // if (exists)
            //     throw new InvalidOperationException("This pokémon already exists");

            pokemon.Value = Pokemon.GetDefaultValue(pokemon.Rarity);
            _context.Pokemons.Add(pokemon);
            _context.PokemonCenter.Add(new PokemonCenter { Pokemon = pokemon });
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

            var exists = dto.OwnerId != null ? await _context.Users.AnyAsync(u => u.Id == dto.OwnerId) : true;
            if (!exists)
                throw new KeyNotFoundException("User not found");

            var pokemon = await _context.Pokemons.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == targetId);

            if (pokemon == null)
                throw new KeyNotFoundException("Pokemon not Found");

            if (!string.IsNullOrEmpty(dto.Name))
                pokemon.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Type))
                pokemon.Type = dto.Type;

            if (!string.IsNullOrEmpty(dto.Nature))
                pokemon.Nature = dto.Nature;

            if (dto.Rarity.HasValue)
                pokemon.Rarity = dto.Rarity.Value;

            if (dto.Value.HasValue)
                pokemon.Value = dto.Value.Value;
            else
                pokemon.Value = Pokemon.GetDefaultValue(pokemon.Rarity);

            pokemon.OwnerId = dto.OwnerId;

            await _context.SaveChangesAsync();

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

        //pokemon center management
        public async Task<ResultDto<PokemonCenter>> DeletePokemonCenter(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            // var targetPokemonCenter = await _context.PokemonCenter.FindAsync(targetId); -> Lazy Loading

            // var targetPokemonCenter = await _context.PokemonCenter.Include(pc => pc.Pokemon).FirstOrDefaultAsync(pc => pc.Id == targetId); -> Eager Loading\

            var targetPokemonCenter = await _context.PokemonCenter.FindAsync(targetId);

            if (targetPokemonCenter == null)
                throw new KeyNotFoundException("Pokémon does not exist in the store");

            await _context.Entry(targetPokemonCenter).Reference(pc => pc.Pokemon).LoadAsync(); //Explicit Loading

            _context.PokemonCenter.Remove(targetPokemonCenter);
            await _context.SaveChangesAsync();

            return new ResultDto<PokemonCenter>
            {
                Message = "'Delete' succeed",

                TargetEntity = targetPokemonCenter
            };
        }

        public async Task<ResultDto<PokemonCenter>> RestorePokemonCenter(string superPassword, PokemonCenterDto dto)
        {
            await AdminAsync(superPassword);

            var pokemonWish = await _context.Pokemons.FindAsync(dto.PokemonId);

            if (pokemonWish == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            var exists = await _context.PokemonCenter.AnyAsync(pc => pc.PokemonId == dto.PokemonId);

            if (exists)
                throw new InvalidOperationException("Pokémon is in Center already");

            PokemonCenter pokemonCenter = new()
            {
                Id = dto.PokemonCenterId,

                PokemonId = dto.PokemonId
            };

            _context.PokemonCenter.Add(pokemonCenter);
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