using System.ComponentModel.DataAnnotations;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class PokemonDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Nature { get; set; }

        [Required]
        [MinLength(2)]
        public List<Elements> Elements { get; set; }

        public int RarityId { get; set; }
        
        public int? OwnerId { get; set; }
    }
}