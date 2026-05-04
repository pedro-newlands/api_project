using PokeShop.Application.DTOs.Management;
using System.Linq.Expressions;

namespace PokeShop.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        // --- User Management ---

        public async Task<ResultDto<IEnumerable<UserManagementResponseDto>>> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();

            var usersProjected = users
                .Select(u => new UserManagementResponseDto(
                    u.Id, 
                    u.UserName, 
                    u.Coins, 
                    u.FirstLogin, 
                    u.IsActive
                ))
                .ToList();

            return new ResultDto<IEnumerable<UserManagementResponseDto>> 
            (
                "'Get' Succeed", 
                usersProjected 
            );
        }

        public async Task<ResultDto<UserManagementResponseDto>> GetUserByIdAsync(int targetId)
        {
            var user = await _repository.GetUserByIdAsync(targetId) 
                ?? throw new KeyNotFoundException("User not found");
            
            return new ResultDto<UserManagementResponseDto> 
            ( 
                "'Get' Succeed", 
                new UserManagementResponseDto(
                    user.Id, 
                    user.UserName, 
                    user.Coins, 
                    user.FirstLogin, 
                    user.IsActive
                ) 
            );
        }

        public async Task<ResultDto<UserManagementResponseDto>> CreateUserAsync(UserManagementCreateDto dto)
        {
            if (await _repository.UserExistsByNameAsync(dto.UserName))
                throw new InvalidOperationException("User with this name already exists");

            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = dto.PasswordHash,
                Coins = dto.Coins,
                FirstLogin = dto.FirstLogin,
                IsActive = true
            };

            var createdUser = await _repository.CreateUserAsync(user);
            return new ResultDto<UserManagementResponseDto> 
            (
                "'Post' Succeed", 
                new UserManagementResponseDto(
                    createdUser.Id, 
                    createdUser.UserName, 
                    createdUser.Coins, 
                    createdUser.FirstLogin, 
                    createdUser.IsActive
                )
            );
        }

        public async Task<ResultDto<UserManagementResponseDto>> UpdateUserAsync(int targetId, UserManagementUpdateDto dto)
        {
            var user = await _repository.GetUserByIdAsync(targetId)
                ?? throw new KeyNotFoundException("User not found");

            user.UserName = dto.UpUsername ?? user.UserName;
            user.Coins = dto.UpCoins ?? user.Coins;
            user.FirstLogin = dto.FirstLogin ?? user.FirstLogin;
            user.IsActive = dto.IsActive ?? user.IsActive;

            var updatedUser = await _repository.UpdateUserAsync(user);

            return new ResultDto<UserManagementResponseDto> 
            (
                "'Update' Succeed", 
                new UserManagementResponseDto(
                    updatedUser.Id, 
                    updatedUser.UserName, 
                    updatedUser.Coins, 
                    updatedUser.FirstLogin, 
                    updatedUser.IsActive
                )
            );
        }

        public async Task<ResultDto<UserManagementResponseDto>> DeleteUserAsync(int targetId)
        {
            if (targetId == 1) // Regra de negócio: Protege o Admin principal
                throw new InvalidOperationException("Admin can not be deactivated");

            var user = await _repository.GetUserByIdAsync(targetId)
                ?? throw new KeyNotFoundException("User not found");

            user.IsActive = false;
            var disabledUser = await _repository.UpdateUserAsync(user);

            return new ResultDto<UserManagementResponseDto> 
            (
                "'Delete' Succeed", 
                new UserManagementResponseDto(
                    disabledUser.Id, 
                    disabledUser.UserName, 
                    disabledUser.Coins, 
                    disabledUser.FirstLogin, 
                    disabledUser.IsActive
                )
            );
        }

        // --- Pokemon Management ---

        public async Task<ResultDto<IEnumerable<PokemonManagementResponseDto>>> GetAllPokemonsAsync()
        {
            var pokemons = await _repository.GetPokemonsAsync();
            var pokemonsProjected = pokemons
                .Select(p => new PokemonManagementResponseDto(
                    p.Id,
                    p.Name,
                    p.Nature,
                    p.Elements,
                    p.RarityId,
                    p.OwnerId
                ))
                .ToList();

             return new ResultDto<IEnumerable<PokemonManagementResponseDto>> 
            (
                "'Get' Succeed", 
                pokemonsProjected
            );
        }

        public async Task<ResultDto<PokemonManagementResponseDto>> GetPokemonByIdAsync(int targetId)
        {
            var pokemon = await _repository.GetPokemonByIdAsync(targetId)
                ?? throw new KeyNotFoundException("Pokemon not found");

            return new ResultDto<PokemonManagementResponseDto> 
            (
                "'Get' Succeed", 
                new PokemonManagementResponseDto(
                    pokemon.Id,
                    pokemon.Name,
                    pokemon.Nature,
                    pokemon.Elements,
                    pokemon.RarityId,
                    pokemon.OwnerId
                )
            );
        }

        public async Task<ResultDto<PokemonManagementResponseDto>> CreatePokemonAsync(PokemonManagementCreateDto dto)
        {
            if (dto.OwnerId.HasValue && !await _repository.UserExistsByIdAsync(dto.OwnerId.Value))
                throw new KeyNotFoundException("Owner not found");

            var pokemon = new Pokemon
            {
                Name = dto.Name,
                Nature = dto.Nature,
                RarityId = dto.RarityId,
                OwnerId = dto.OwnerId,
                Elements = await _repository.GetElementsByNames(dto.Elements)
            };

            var createdPokemon = await _repository.CreatePokemonAsync(pokemon);

            return new ResultDto<PokemonManagementResponseDto> 
            (
                "'Post' Succeed", 
                new PokemonManagementResponseDto(
                    createdPokemon.Id,
                    createdPokemon.Name,
                    createdPokemon.Nature,
                    createdPokemon.Elements,
                    createdPokemon.RarityId,
                    createdPokemon.OwnerId
                )
            );
        }

        public async Task<ResultDto<PokemonManagementResponseDto>> UpdatePokemonAsync(int targetId, PokemonManagementUpdateDto dto)
        {
            var pokemon = await _repository.GetPokemonByIdAsync(targetId)
                ?? throw new KeyNotFoundException("Pokemon not found");

            if (dto.UpOwnerId.HasValue && dto.UpOwnerId != pokemon.OwnerId)
            {
                if (!await _repository.UserExistsByIdAsync(dto.UpOwnerId.Value))
                    throw new KeyNotFoundException("New owner not found");

                await _repository.CreateTransactionAsync(new Transaction
                {
                    UserId = dto.UpOwnerId.Value,
                    PokemonId = targetId,
                    Status = TransactionStatus.Transferred,
                    CoinsAdjustment = "Admin Action",
                });
            }

            pokemon.Name = dto.UpName ?? pokemon.Name;
            pokemon.Nature = dto.UpNature ?? pokemon.Nature;
            pokemon.RarityId = dto.UpRarityId ?? pokemon.RarityId;
            pokemon.OwnerId = dto.UpOwnerId;

            if (dto.UpElements != null)
            {
                var elements = await _repository.GetElementsByNames(dto.UpElements);
                pokemon.Elements.Clear();
                foreach (var el in elements) pokemon.Elements.Add(el);
            }

            var updatedPokemon = await _repository.UpdatePokemonAsync(pokemon);

            return new ResultDto<PokemonManagementResponseDto> 
            (
                "'Update' Succeed", 
                new PokemonManagementResponseDto(
                    updatedPokemon.Id,
                    updatedPokemon.Name,
                    updatedPokemon.Nature,
                    updatedPokemon.Elements,
                    updatedPokemon.RarityId,
                    updatedPokemon.OwnerId
                )
            );
        }

        public async Task<ResultDto<PokemonManagementResponseDto>> DeletePokemonAsync(int targetId)
        {
            var pokemon = await _repository.GetPokemonByIdAsync(targetId)
                ?? throw new KeyNotFoundException("Pokémon does not exist");

            await _repository.DeletePokemonAsync(pokemon);
            
            return new ResultDto<PokemonManagementResponseDto> 
            (
                "'Delete' Succeed", 
                new PokemonManagementResponseDto(
                    pokemon.Id,
                    pokemon.Name,
                    pokemon.Nature,
                    pokemon.Elements,
                    pokemon.RarityId,
                    pokemon.OwnerId
                )
            );
        }

        // --- Pokemon Center Management ---

        public async Task<ResultDto<PokemonCenterManagementResponseDto>> CreatePokemonCenterAsync(PokemonCenterManagementCreateDto dto)
        {
            var pokemon = await _repository.GetPokemonByIdAsync(dto.PokemonId)
                ?? throw new KeyNotFoundException("Pokémon does not exist");

            if (await _repository.PokemonCenterExistsById(dto.PokemonId))
                throw new InvalidOperationException("Pokémon is in Center already");

            var centerEntry = new PokemonCenter
            {
                PokemonId = dto.PokemonId,
                MarketPrice = dto.MarketPrice ?? pokemon.Rarity.Price
            };

            var createdPokemonCenter = await _repository.CreatePokemonCenterAsync(centerEntry);

            return new ResultDto<PokemonCenterManagementResponseDto> 
            (
                "'Post' Succeed", 
                new PokemonCenterManagementResponseDto(
                    createdPokemonCenter.PokemonId,
                    createdPokemonCenter.MarketPrice
                )
            );
        }

        public async Task<ResultDto<PokemonCenterManagementResponseDto>> UpdatePokemonCenterMarketPriceAsync(int targetId, PokemonCenterManagementUpdateDto dto)
        {
            var centerEntry = await _repository.GetPokemonCenterByIdAsync(targetId)
                ?? throw new KeyNotFoundException("Pokémon not in store");

            if (dto.UpMarketPrice == null)
            {
                var pokemon = await _repository.GetPokemonByIdAsync(targetId);
                centerEntry.MarketPrice = pokemon?.Rarity.Price ?? 0;
            }
            else
            {
                centerEntry.MarketPrice = dto.UpMarketPrice.Value;
            }

            var updatedPokemonCenter = await _repository.UpdatePokemonCenterAsync(centerEntry);

            return new ResultDto<PokemonCenterManagementResponseDto> 
            (
                "'Update' Succeed", 
                new PokemonCenterManagementResponseDto(
                    updatedPokemonCenter.PokemonId,
                    updatedPokemonCenter.MarketPrice
                )
            );
        }

        public async Task<ResultDto<PokemonCenterManagementResponseDto>> DeletePokemonCenterAsync(int targetId)
        {
            var pokemonCenter = await _repository.GetPokemonCenterByIdAsync(targetId)
                ?? throw new KeyNotFoundException("Pokémon not in store");

            await _repository.DeletePokemonCenterAsync(pokemonCenter);

            return new ResultDto<PokemonCenterManagementResponseDto> 
            (
                "'Delete' Succeed", 
                new PokemonCenterManagementResponseDto(
                    pokemonCenter.PokemonId,
                    pokemonCenter.MarketPrice
                )
            );
        }

        // --- Transactions ---
        public async Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsHistoryAsync(int? year = null, int? month = null, int? day = null)
        {
            /*Expression<Func<Transaction, bool>> filter = (t) => (
                !year.HasValue || t.TransactionDate == year) && 
                (!month.HasValue || t.TransactonDate == month) &&  
                (!day.HasValue || t.TransactonDate == day);
            */

            ParameterExpression param = Expression.Parameter(typeof(Transaction), "t");
            MemberExpression dateProperty = Expression.Property(param, "TransactionDate");
            Expression filterBody = null;

            //busca por intervalo (SARGable - Performance máxima)
            if (year.HasValue)
            {
                DateTime start = new DateTime(year.Value, month ?? 1, day ?? 1);
                DateTime end;

                if (day.HasValue && month.HasValue)
                    end = start.AddDays(1);
                else if (month.HasValue)
                    end = start.AddMonths(1);
                else   
                    end = start.AddYears(1);

                ConstantExpression startConstant = Expression.Constant(start);
                ConstantExpression endConstant = Expression.Constant(end);

                filterBody = Expression.AndAlso(
                    Expression.GreaterThanOrEqual(dateProperty, startConstant),
                    Expression.LessThan(dateProperty, endConstant)
                );
            }

            //busca sem ano (não usa índices de forma eficiente)
            else
            {
                if (month.HasValue)
                {
                    var monthProp = Expression.Property(dateProperty, "Month");
                    filterBody = Expression.Equal(monthProp, Expression.Constant(month.Value));
                }
                
                if (day.HasValue)
                {
                    var dayProp = Expression.Property(dateProperty, "Day");
                    var dayIsEqual = Expression.Equal(dayProp, Expression.Constant(day.Value));
                    filterBody = filterBody == null ? dayIsEqual : Expression.AndAlso(filterBody, dayIsEqual);
                }
            }
            
            if (filterBody == null) filterBody = Expression.Constant(true);

            var expressionTree = Expression.Lambda<Func<Transaction, bool>>(filterBody, param);
            
            var transactionsFiltered = await _repository.GetTransactionsHistoryAsync(expressionTree);

            var transactionsFilteredProjected = transactionsFiltered
                .Select(t => new TransactionManagementResponseDto(
                    t.Id,
                    t.UserId,
                    t.PokemonId,
                    t.TransactionDate,
                    t.Status
                ))
                .ToList();

            return new ResultDto<IEnumerable<TransactionManagementResponseDto>> 
            (
                "'Get' Succeed", 
                transactionsFilteredProjected
            );
        }

        public async Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetAllTransactionsAsync()
        {
            var transactions = await _repository.GetTransactionsAsync();

            var transactionsProjected = transactions
                .Select(t => new TransactionManagementResponseDto(
                    t.Id,
                    t.UserId,
                    t.PokemonId,
                    t.TransactionDate,
                    t.Status
                ))
                .ToList();
            
            return new ResultDto<IEnumerable<TransactionManagementResponseDto>>
            (
                "'Get succeed'",
                transactionsProjected
            );
        }

        public async Task<ResultDto<TransactionManagementResponseDto>> GetTransactionByIdAsync(int id)
        {
            var transaction = await _repository.GetTransactionByIdAsync(id);

            return new ResultDto<TransactionManagementResponseDto>
            (
                "'Get succeed'",
                new TransactionManagementResponseDto(
                    transaction.Id,
                    transaction.UserId,
                    transaction.PokemonId,
                    transaction.TransactionDate,
                    transaction.Status
                )
            );
        }

        public async Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsByUserIdAsync(int id)
        {
            var transactions = await _repository.GetTransactionsByUserIdAsync(id);

            var transactionsProjected = transactions
                .Select(t => new TransactionManagementResponseDto(
                    t.Id,
                    t.UserId,
                    t.PokemonId,
                    t.TransactionDate,
                    t.Status
                ))
                .ToList();
            
            return new ResultDto<IEnumerable<TransactionManagementResponseDto>>
            (
                "'Get succeed'",
                transactionsProjected
            );
        }

        public async Task<ResultDto<IEnumerable<TransactionManagementResponseDto>>> GetTransactionsByPokemonIdAsync(int id)
        {
            var transactions = await _repository.GetTransactionsByPokemonIdAsync(id);

            var transactionsProjected = transactions
                .Select(t => new TransactionManagementResponseDto(
                    t.Id,
                    t.UserId,
                    t.PokemonId,
                    t.TransactionDate,
                    t.Status
                ))
                .ToList();
            
            return new ResultDto<IEnumerable<TransactionManagementResponseDto>>
            (
                "'Get succeed'",
                transactionsProjected
            );
        }
    }
}