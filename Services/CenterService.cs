using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Repositories;

namespace ProjetoPokeShop.Services
{
    public class CenterService : ICenterService
    {
        private readonly CenterRepository _repository;
        private static readonly Random _rng = new();

        public CenterService(CenterRepository repository) => _repository = repository;

        public async Task<IEnumerable<AvailablePokemonDto>> GetAvailablePokemonsAsync()
        {
            return await _repository.GetAvailablePokemonsAsync();
        }

        public async Task<BuyResultDto> BuyPokemonAsync(int pokemonCenterId, int userId)
        {
<<<<<<< HEAD
            var outPokemon = await _repository.GetPokemonCenterByIdAsync(pokemonCenterId)
                ?? throw new KeyNotFoundException("Pokémon not found");

            if (outPokemon.Pokemon.OwnerId != null)
=======
            var outPokemonCenter = await _repository.GetPokemonCenterByIdAsync(pokemonCenterId)
                ?? throw new KeyNotFoundException("Pokémon not found");

            if (outPokemonCenter.Pokemon.OwnerId != null)
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                throw new InvalidOperationException("Pokémon is no longer available");

            var user = await _repository.GetUserByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

<<<<<<< HEAD
            if (user.Coins < outPokemon.Pokemon.Value)
                throw new InvalidOperationException("Not enough coins");

            await _repository.BuyPokemonAsync(outPokemon, user);

            return new BuyResultDto
            {
                OwnerId = user.Id,
                PokemonName = outPokemon.Pokemon.Name,
                Nature = outPokemon.Pokemon.Nature,
                Type = outPokemon.Pokemon.Type,
                Rarity = outPokemon.Pokemon.Rarity,
                PokemonMarketValue = outPokemon.Pokemon.Value,
                CoinsAdjustment = $"- {outPokemon.Pokemon.Value:C0}"
=======
            if (user.Coins < outPokemonCenter.MarketPrice)
                throw new InvalidOperationException("Not enough coins");

            await _repository.BuyPokemonAsync(outPokemonCenter, user);

            var value = outPokemonCenter.MarketPrice;

            return new BuyResultDto
            {
                OwnerId = user.Id,
                PokemonName = outPokemonCenter.Pokemon.Name,
                Nature = outPokemonCenter.Pokemon.Nature,
                Elements = outPokemonCenter.Pokemon.Elements.Select(e => e.Name).ToList(),
                Rarity = outPokemonCenter.Pokemon.Rarity.Name,
                MarketValue = outPokemonCenter.MarketPrice,
                CoinsAdjustment = $"-{value:C0}"
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            };
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

            await _repository.BuyPokeballAsync(user, randomPokemon);

            return new PokeballDto
            {
                Pokeball = "Poké Ball",
                Rarity = rarity,
                PokemonName = randomPokemon.Name,
                Nature = randomPokemon.Nature,
<<<<<<< HEAD
                Type = randomPokemon.Type,
                PokemonMarketValue = randomPokemon.Value,
=======
                Elements = randomPokemon.Elements.Select(e => e.Name).ToList(),
                MarketValue = randomPokemon.Rarity.Price,
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                OwnerId = user.Id,
                CoinsAdjustment = $"- {pokeballCost}"
            };
        }

<<<<<<< HEAD
        private PokemonRarity RollRarity()
=======
        private Rarities RollRarity()
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        {
            var random = _rng.Next(100);
            return random switch
            {
<<<<<<< HEAD
                < 45 => PokemonRarity.Common,
                < 75 => PokemonRarity.Uncommon,
                < 95 => PokemonRarity.Rare,
                _ => PokemonRarity.Legendary
=======
                < 45 => Rarities.Common,
                < 75 => Rarities.Uncommon,
                < 95 => Rarities.Rare,
                _ => Rarities.Legendary
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
            };
        }
    }
}