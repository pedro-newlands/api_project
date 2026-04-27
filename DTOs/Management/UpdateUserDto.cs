using System.ComponentModel.DataAnnotations;

namespace ProjetoPokeShop.DTOs
{
    public class UpdateUserDto
    {     
        public string? UpUsername { get; set; }

        public int? UpCoins { get; set; }

        public bool? FirstLogin { get; set; }

        public bool? IsActive {get; set; }
    }
}