using PokeShop.Application.DTOs.Storage;

namespace PokeShop.Application.Services
{
    public class StorageService : IStorageService
    {
        readonly IStorageRepository _repository;

        public StorageService(IStorageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EngagedPokemonDto>> GetInventoryAsync(int userId)
        {
            if (!await _repository.UserExistsByIdAsync(userId))
                throw new KeyNotFoundException("User does not exist");

            if (!await _repository.UserOwnsSomeInventary(userId))
                throw new KeyNotFoundException("No inventory for this user");

            var storage = await _repository.GetUserInventoryAsListAsync(userId);

            if (!storage.Any())
                return Enumerable.Empty<EngagedPokemonDto>();

            return storage
                .Select(p => new EngagedPokemonDto(
                    p.Id,
                    p.Name,
                    p.Nature,
                    p.Elements.Select(e => e.Name).ToList(),
                    p.Rarity.Price,
                    p.Rarity.Name
                ))
                .ToList();
        }

        public async Task<IEnumerable<TransactionSummaryDto>> GetTransactionsAsync(int userId)
        {
            if (!await _repository.UserExistsByIdAsync(userId))
                throw new KeyNotFoundException("User does not exist");

            var transactions = await _repository.GetTransactionsAsListAsync(userId);

            var transactionsProjected = transactions
                .Select(t => new TransactionSummaryDto(
                    t.Pokemon.Name,
                    t.Status,
                    t.TransactionDate,
                    t.CoinsAdjustment
                ))
                .ToList();
            return transactionsProjected;
        }

        public async Task<SellResultDto> SellPokemonAsync(int userId, int pokemonId)
        {

            var pokemon = await _repository.GetPokemonById(pokemonId);

            if (pokemon == null)
                throw new KeyNotFoundException("Pokémon does not exist");

            if (pokemon.OwnerId != userId)
                throw new InvalidOperationException("This pokémon does not belong to this user");

            var user = await _repository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User does not exist");

            int pokemonPrice = pokemon.Rarity.Price;

            Transaction transaction = new()
            {
                UserId = user.Id,
                PokemonId = pokemon.Id,
                Status = TransactionStatus.Sold,
                CoinsAdjustment = $"+ {pokemonPrice:C0}"
            };

            var returnPokemonToCenter = await _repository.GetPokemonCenterByIdAsync(pokemonId);
            if (returnPokemonToCenter != null)
            {
                returnPokemonToCenter.MarketPrice =  pokemonPrice;
                await _repository.SaveSellAsync(transaction);
            } else
            {
                returnPokemonToCenter = new PokemonCenter()
                {
                    PokemonId = pokemon.Id,
                    MarketPrice = pokemonPrice
                };
                await _repository.SaveSellAsync(transaction, returnPokemonToCenter);
            }

            return new SellResultDto(
                user.UserName,
                pokemon.Name,
                pokemon.Nature,
                pokemon.Elements.Select(e => e.Name).ToList(),
                pokemon.Rarity.Name,
                pokemonPrice,
                $"+ {pokemonPrice:C0}"
            );
        }
    }
}
