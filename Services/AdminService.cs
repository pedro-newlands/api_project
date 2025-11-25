using ProjetoPokeShop.DTOs;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Repositories;

namespace ProjetoPokeShop.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminRepository _repository;

        public AdminService(AdminRepository repository)
        {
            _repository = repository;
        }

        //user management
        public async Task<ResultDto<IEnumerable<User>>> GetAllUsersAsync(string superPassword)
        {
            await AdminAsync(superPassword);

            var users = await _repository.GetUsersAsync();

            return new ResultDto<IEnumerable<User>>
            {
                Message = "'Get' Succeed",
                TargetEntity = users
            };
        }

        public async Task<ResultDto<User>> GetUserByIdAsync(string superPassword, int id)
        {
            await AdminAsync(superPassword);

            var user = await _repository.GetUserByIdAsync(id);
            if (user is null)
                throw new KeyNotFoundException("User not found");
            
            return new ResultDto<User>
            {
                Message = "'Get' Succeed",
                TargetEntity = user
            };
        }

        public async Task<ResultDto<User>> CreateUserAsync(string superPassword, UserDto dto)
        {
            await AdminAsync(superPassword);

            if (await _repository.UserExistsByNameAsync(dto.UserName))
                throw new InvalidOperationException("User with this name already exists");

            User user = new()
            {
                UserName = dto.UserName,
                PasswordHash = dto.PasswordHash,
                Coins = dto.Coins,
                FirstLogin = dto.FirstLogin
            };

            await _repository.CreateUserAsync(user);

            return new ResultDto<User>
            {
                Message = "'Post' Succeed",
                TargetEntity = user
            };
        }

        public async Task<ResultDto<User>> UpdateUserAsync(string superPassword, int id, UpdateUserDto dto)
        {
            await AdminAsync(superPassword);

            var user = await _repository.GetUserByIdAsync(id);
            if (user is null)
                throw new KeyNotFoundException("User not found");   
            var updatedUser = await _repository.UpdateUserAsync(user, dto);

            return new ResultDto<User>
            {
                Message = "'Update' Succeed",
                TargetEntity = updatedUser
            };
        }

        public async Task<ResultDto<User>> DeleteUserAsync(string superPassword, int id)
        {
            await AdminAsync(superPassword);

            var user = await _repository.GetUserByIdAsync(id);
            if (user is null)
                throw new KeyNotFoundException("User not found");

            await _repository.DeleteUserAsync(user);

            return new ResultDto<User>
            {
                Message = "'Delete' Succeed",
                TargetEntity = user
            };
        }

        //pokemon management
         public async Task<ResultDto<IEnumerable<Pokemon>>> GetAllPokemonsAsync(string superPassword)
        {
            await AdminAsync(superPassword);

            var pokemons = await _repository.GetPokemonsAsync();

            return new ResultDto<IEnumerable<Pokemon>>
            {
                Message = "'Get' Succeed",
                TargetEntity = pokemons
            };
        }

        public async Task<ResultDto<Pokemon>> GetPokemonByIdAsync(string superPassword, int id)
        {
            await AdminAsync(superPassword);

            var pokemon = await _repository.GetPokemonByIdAsync(id);
            if (pokemon is null)
                throw new KeyNotFoundException("Pokemon not found");

            return new ResultDto<Pokemon>
            {
                Message = "'Get' Succeed",
                TargetEntity = pokemon
            };;
        }

        public async Task<ResultDto<Pokemon>> CreatePokemonAsync(string superPassword, PokemonDto dto)
        {
            await AdminAsync(superPassword);

            if (dto.OwnerId is not null)
            {
                var ownerExists = await _repository.UserExistsByIdAsync(dto.OwnerId.Value);
                if (!ownerExists)
                    throw new KeyNotFoundException("Owner not found");
            }

            Pokemon pokemon = new()
            {
                Name = dto.Name,
                Type = dto.Type,
                Nature = dto.Nature,
                Rarity = dto.Rarity,
                Value = Pokemon.GetDefaultValue(dto.Rarity),
                OwnerId = dto.OwnerId
            };
            var newPokemon = await _repository.CreatePokemonAsync(pokemon);

            if (dto.OwnerId is not null)
            {
                UserPokemon userPokemon = new()
                {
                    UserId = dto.OwnerId.Value,
                    PokemonId = pokemon.Id
                };
                await _repository.CreateUserPokemonAsync(userPokemon);
            }

            PokemonCenter center = new()
            {
                PokemonId = pokemon.Id
            };
            await _repository.CreatePokemonCenterAsync(center);

            return new ResultDto<Pokemon>
            {
                Message = "'Post' Succeed",
                TargetEntity = newPokemon
            };
        }

        public async Task<ResultDto<Pokemon>> UpdatePokemonAsync(string superPassword, int id, UpdatePokemonDto dto)
        {
            await AdminAsync(superPassword);

            var pokemon = await _repository.GetPokemonByIdAsync(id);
            if (pokemon is null)
                throw new KeyNotFoundException("Pokemon not found");

            if (dto.OwnerId is not null && !await _repository.UserExistsByIdAsync(dto.OwnerId.Value))
                throw new KeyNotFoundException("New owner not found");

            var userPokemon = await _repository.GetUserPokemonByPokemonIdAsync(id);

            if (dto.OwnerId != null)
            {
                if (userPokemon != null)
                    userPokemon.UserId = dto.OwnerId.Value;
                else
                {
                    await _repository.CreateUserPokemonAsync(new UserPokemon
                    {
                        UserId = dto.OwnerId.Value,
                        PokemonId = pokemon.Id,
                    });
                }
            }
            else if (userPokemon != null)
            {
                await _repository.DeleteUserPokemonAsync(userPokemon);
            }

            await _repository.UpdatePokemonAsync(pokemon, dto);
            var updatedPokemon = await _repository.GetPokemonByIdAsync(pokemon.Id);

            return new ResultDto<Pokemon>
            {
                Message = "'Update' Succeed",
                TargetEntity = updatedPokemon
            };
        }

        public async Task<ResultDto<Pokemon>> DeletePokemonAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetPokemon = await _repository.GetPokemonByIdAsync(targetId);

            if (targetPokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            await _repository.DeletePokemonAsync(targetPokemon);

            return new ResultDto<Pokemon>
            {
                Message = "'Delete' succeed",
                TargetEntity = targetPokemon
            };
        }

        //pokemonCenter management
        public async Task<ResultDto<PokemonCenter>> DeletePokemonCenterAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetPokemonCenter = await _repository.GetPokemonCenterByIdAsync(targetId);

            if (targetPokemonCenter == null)
                throw new KeyNotFoundException("Pokémon does not exist in the store");

            await _repository.DeletePokemonCenterAsync(targetPokemonCenter);

            return new ResultDto<PokemonCenter>
            {
                Message = "'Delete' succeed",

                TargetEntity = targetPokemonCenter
            };
        }

        public async Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(string superPassword, PokemonCenter pokemonCenter)
        {
            await AdminAsync(superPassword);

            var pokemonWish = await _repository.GetPokemonByIdAsync(pokemonCenter.PokemonId);

            if (pokemonWish == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            var exists = await _repository.PokemonCenterExistsByPokemonId(pokemonCenter.PokemonId);

            if (exists)
                throw new InvalidOperationException("Pokémon is in Center already");

            await _repository.CreatePokemonCenterAsync(pokemonCenter);

            return new ResultDto<PokemonCenter>
            {
                Message = "'Post' succeed",
                TargetEntity = pokemonCenter
            };
        }

        private async Task AdminAsync(string superPassword)
        {
            var admin = await _repository.GetUserByIdAsync(1);
        
            if (admin.PasswordHash != superPassword)
                throw new InvalidOperationException("Not allowed");
        }
    }
}