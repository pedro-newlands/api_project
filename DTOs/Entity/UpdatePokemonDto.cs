using System.ComponentModel.DataAnnotations;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class UpdatePokemonDto
    {
        public string? Name { get; set; }

        public string? Nature { get; set; }

        public string? Type { get; set; }

        public PokemonRarity? Rarity { get; set; }

        public int? Value { get; set; }

        public int? OwnerId { get; set; }
    }
}