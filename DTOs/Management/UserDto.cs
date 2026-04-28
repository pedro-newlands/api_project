using System.ComponentModel.DataAnnotations;

namespace ProjetoPokeShop.DTOs.Entity
{
    public class UserDto
    {
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MinLength(2)]
        public string PasswordHash { get; set; } 

        public int Coins { get; set; }

        public bool FirstLogin { get; set; } 
    }
}