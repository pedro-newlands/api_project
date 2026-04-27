using ProjetoPokeShop.DTOs;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Repositories;
using api_project.DTOs.Management;

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

        public async Task<ResultDto<User>> GetUserByIdAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var user = await _repository.GetUserByIdAsync(targetId);
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

        public async Task<ResultDto<User>> UpdateUserAsync(string superPassword, int targetId, UpdateUserDto dto)
        {
            await AdminAsync(superPassword);

            var targetUser = await _repository.GetUserByIdAsync(targetId);
            if (targetUser is null)
                throw new KeyNotFoundException("User not found");   
            var updatedUser = await _repository.UpdateUserAsync(targetUser, dto);

            return new ResultDto<User>
            {
                Message = "'Update' Succeed",
                TargetEntity = updatedUser
            };
        }

        public async Task<ResultDto<User>> DeleteUserAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            if (targetId == 1)
                throw new InvalidOperationException("Admin can not be deactivated");

            var targetUser = await _repository.GetUserByIdAsync(targetId) 
                ?? throw new KeyNotFoundException("User not found");

            await _repository.UpdateUserAsync(targetUser, new UpdateUserDto
            {
                IsActive = false
            });

            return new ResultDto<User>
            {
                Message = "'Delete' (Deactivation) Succeed",
                TargetEntity = targetUser
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

        public async Task<ResultDto<Pokemon>> GetPokemonByIdAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var pokemon = await _repository.GetPokemonByIdAsync(targetId);
            if (pokemon is null)
                throw new KeyNotFoundException("Pokemon not found");

            return new ResultDto<Pokemon>
            {
                Message = "'Get' Succeed",
                TargetEntity = pokemon
            };
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
                Elements = await _repository.GetElementsByNames(dto.Elements),
                Nature = dto.Nature,
                RarityId = dto.RarityId,
                OwnerId = dto.OwnerId
            };
            var newPokemon = await _repository.CreatePokemonAsync(pokemon);

            return new ResultDto<Pokemon>
            {
                Message = "'Post' Succeed",
                TargetEntity = newPokemon
            };
        }

        public async Task<ResultDto<Pokemon>> UpdatePokemonAsync(string superPassword, int targetId, UpdatePokemonDto dto)
        {
            await AdminAsync(superPassword);

            var targetPokemon = await _repository.GetPokemonByIdAsync(targetId);
            if (targetPokemon is null)
                throw new KeyNotFoundException("Pokemon not found");

            if (dto.OwnerId is not null && !await _repository.UserExistsByIdAsync(dto.OwnerId.Value))
                throw new KeyNotFoundException("New owner not found");

            if (dto.OwnerId != targetPokemon.OwnerId)
            {
               Transaction transaction = new()
               {
                   UserId = dto.OwnerId.Value,
                   PokemonId = targetId,
                   Status = TransactionStatus.Transferred,
                   CoinsAdjustment = "",
                   TransactionDate = DateTime.UtcNow
               };

               await _repository.CreateTransactionAsync(transaction);
            }

            var updatedPokemon = await _repository.UpdatePokemonAsync(targetPokemon, dto);

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

        public async Task<ResultDto<PokemonCenter>> CreatePokemonCenterAsync(string superPassword, PokemonCenterDto dto)
        {
            await AdminAsync(superPassword);

            var pokemon = await _repository.GetPokemonByIdAsync(dto.PokemonId);

            if(pokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            var existsInCenter = await _repository.PokemonCenterExistsById(dto.PokemonId);

            if (existsInCenter)
                throw new InvalidOperationException("Pokémon is in Center already");

            PokemonCenter pokemonCenter = new()
            {
                PokemonId = dto.PokemonId,
                MarketPrice = dto.MarketPrice ?? pokemon.Rarity.Price
            };

            var createdPokemonCenter = await _repository.CreatePokemonCenterAsync(pokemonCenter);

            return new ResultDto<PokemonCenter>
            {
                Message = "'Post' succeed",
                TargetEntity = createdPokemonCenter
            };

        }

        public async Task<ResultDto<PokemonCenter>> UpdatePokemonCenterMarketPriceAsync(string superPassword, int targetId, UpdatePriceDto dto)
        {
            await AdminAsync(superPassword);

            var targetPokemonCenter = await _repository.GetPokemonCenterByIdAsync(targetId);

            if (targetPokemonCenter == null)
            {
                throw new KeyNotFoundException("Pokémon does not exist in the store");
            }

            if (dto.MarketPrice == null)
            {
                var pokemon = await _repository.GetPokemonByIdAsync(targetId);

                dto.MarketPrice = pokemon?.Rarity.Price;
            }

            var updatedPokemonCenter = await _repository.UpdatePokemonCenterMarketPriceAsync(targetPokemonCenter, dto.MarketPrice);

            return new ResultDto<PokemonCenter>
            {
                Message = "'Update' Succeed",
                TargetEntity = updatedPokemonCenter
            };
        }

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

        public async Task<ResultDto<IEnumerable<Transaction>>> GetAllTransactionsAsync(string superPassword)
        {
            await AdminAsync(superPassword);

            var transactios = await _repository.GetTransactionsAsync();

            return new ResultDto<IEnumerable<Transaction>> 
            {
                Message = "'Get' Succeed",
                TargetEntity = transactios
            };
        }

        public async Task<ResultDto<Transaction>> GetTransactionByIdAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var transaction = await _repository.GetTransactionByIdAsync(targetId);
            if (transaction is null)
                throw new KeyNotFoundException("Transaction not found");

            return new ResultDto<Transaction>
            {
                Message = "'Get' Succeed",
                TargetEntity = transaction
            };
        }

        public async Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByUserIdAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var transactions = await _repository.GetTransactionsByUserIdAsync(targetId);
            if (transactions is null)
                throw new KeyNotFoundException("Transaction not found");

            return new ResultDto<IEnumerable<Transaction>>
            {
                Message = "'Get' Succeed",
                TargetEntity = transactions
            };
        }

        public async Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsByPokemonIdAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var transactions = await _repository.GetTransactionsByPokemonIdAsync(targetId);
            if (transactions is null)
                throw new KeyNotFoundException("Transaction not found");

            return new ResultDto<IEnumerable<Transaction>>
            {
                Message = "'Get' Succeed",
                TargetEntity = transactions
            };
        }

        public async Task<ResultDto<IEnumerable<Transaction>>> GetTransactionsHistoryAsync(string superPassword, int? year = null, int? month = null, int? day = null)
        {
            await AdminAsync(superPassword);

            IQueryable<Transaction> query = _repository.GetTransactionsHistoryAsync();

            if (year.HasValue)
            {
                query = query.Where(t => t.TransactionDate.Year == year.Value);
            }

            if (month.HasValue)
            {
                query = query.Where(t => t.TransactionDate.Month == month.Value);
            }

            if (day.HasValue)
            {
                query = query.Where(t => t.TransactionDate.Day == day.Value);
            }

            var transactions = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return new ResultDto<IEnumerable<Transaction>>
            {
                Message = "'Get' Succeed",
                TargetEntity = transactions
            };
        }

        public async Task<ResultDto<Transaction>> DeleteTransactionAsync(string superPassword, int targetId)
        {
            await AdminAsync(superPassword);

            var targetTransaction = await _repository.GetTransactionByIdAsync(targetId);

            if (targetTransaction == null)
                throw new KeyNotFoundException("Transaction does not exist");

            await _repository.DeleteTransactionAsync(targetTransaction);

            return new ResultDto<Transaction>
            {
                Message = "'Delete' succeed",
                TargetEntity = targetTransaction
            };
        }

        private async Task AdminAsync(string superPassword)
        {
            var admin = await _repository.GetUserByIdAsync(1);
            
            if (admin == null || admin.PasswordHash != superPassword)
                throw new InvalidOperationException("Not allowed");
        }

    }
}