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
            var outPokemon = await _repository.GetPokemonCenterByIdAsync(pokemonCenterId)
                ?? throw new KeyNotFoundException("Pokémon not found");

            if (outPokemon.Pokemon.OwnerId != null)
                throw new InvalidOperationException("Pokémon is no longer available");

            var user = await _repository.GetUserByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

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
                Type = randomPokemon.Type,
                PokemonMarketValue = randomPokemon.Value,
                OwnerId = user.Id,
                CoinsAdjustment = $"- {pokeballCost}"
            };
        }

        private PokemonRarity RollRarity()
        {
            var random = _rng.Next(100);
            return random switch
            {
                < 45 => PokemonRarity.Common,
                < 75 => PokemonRarity.Uncommon,
                < 95 => PokemonRarity.Rare,
                _ => PokemonRarity.Legendary
            };
        }
    }
}