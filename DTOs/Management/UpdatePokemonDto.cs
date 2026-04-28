using System.ComponentModel.DataAnnotations;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class UpdatePokemonDto
    {
        public string? Name { get; set; }

        public string? Nature { get; set; }

        public List<Elements>? Elements { get; set; }

        public int? RarityId { get; set; }

        public int? OwnerId { get; set; }
    }
}