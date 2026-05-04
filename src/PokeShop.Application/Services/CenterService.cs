using PokeShop.Application.DTOs.Center;

namespace PokeShop.Application.Services
{
    public class CenterService : ICenterService
    {
        readonly ICenterRepository _repository;
        private static readonly Random _rng = new();

        public CenterService(ICenterRepository repository) => _repository = repository;

        public async Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemonsAsync()
        {
            var pokemonsOnCenter = await _repository.GetAvailablePokemonsAsync();

            var pokemonsOnCenterProjected = pokemonsOnCenter
                .Select(pc => new AvailablePokemonDto(
                    pc.PokemonId,
                    pc.Pokemon.Name,
                    pc.Pokemon.Nature,
                    pc.Pokemon.Elements.Select(e => e.Name),
                    pc.MarketPrice,
                    pc.Pokemon.Rarity.Name
                ))
                .ToList();

                return pokemonsOnCenterProjected;
        }

        public async Task<BuyResultDto> BuyPokemonAsync(int pokemonCenterId, int userId)
        {
            var outPokemonCenter = await _repository.GetPokemonCenterByIdAsync(pokemonCenterId)
                ?? throw new KeyNotFoundException("Pokémon not found");

            if (outPokemonCenter.Pokemon.OwnerId != null)
                throw new InvalidOperationException("Pokémon is no longer available");

            var user = await _repository.GetUserByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            if (user.Coins < outPokemonCenter.MarketPrice)
                throw new InvalidOperationException("Not enough coins");

            Transaction transaction = new Transaction()
            {
                UserId = user.Id,
                PokemonId = outPokemonCenter.PokemonId,
                Status = TransactionStatus.Owned,
                CoinsAdjustment = $"- {outPokemonCenter.MarketPrice:C0}"
            };

            await _repository.SavePurchaseAsync(transaction, outPokemonCenter);

            outPokemonCenter.Pokemon.OwnerId = user.Id;

            var value = outPokemonCenter.MarketPrice;

            return new BuyResultDto(
                user.Id,
                outPokemonCenter.Pokemon.Name,
                outPokemonCenter.Pokemon.Nature,
                outPokemonCenter.Pokemon.Elements.Select(e => e.Name).ToList(),
                outPokemonCenter.Pokemon.Rarity.Name,
                value,
                $"- {value:C0}"
            );
        }

        public async Task<PokeballDto> BuyPokeballAsync(int userId)
        {
            var user = await _repository.GetUserByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            const int pokeballCost = 45;

            if (user.Coins < pokeballCost)
                throw new InvalidOperationException("Not enough coins");

            var rarity = RollRarity();

            var pokemons = await _repository.GetAvailablePokemonsByRarityAsync(rarity);

            if (!pokemons.Any())
                throw new InvalidOperationException("No Pokémon available in this rarity");

            var randomPokemon = pokemons[_rng.Next(pokemons.Count)];

            Transaction transaction = new Transaction()
            {
                UserId = user.Id,
                PokemonId = randomPokemon.Id,
                Status = TransactionStatus.Owned,
                CoinsAdjustment = $"- {randomPokemon.Rarity.Price:C0}"
            };

            var pokemonDroped = await _repository.GetPokemonCenterByIdAsync(randomPokemon.Id);


            await _repository.SavePurchaseAsync(transaction, pokemonDroped);

            return new PokeballDto(
                "Poké Ball",
                rarity,
                randomPokemon.Name,
                randomPokemon.Nature,
                randomPokemon.Elements.Select(e => e.Name).ToList(),
                randomPokemon.Rarity.Price,
                user.Id,
                $"- {pokeballCost}"
            );
        }

        private Rarities RollRarity()
        {
            var random = _rng.Next(100);
            return random switch
            {
                < 45 => Rarities.Common,
                < 75 => Rarities.Uncommon,
                < 95 => Rarities.Rare,
                _ => Rarities.Legendary
            };
        }
    }
}