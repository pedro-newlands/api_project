using System.ComponentModel.DataAnnotations;

namespace PokeShop.Application.DTOs.Management
{
    public class UserManagementUpdateDto
    {     
        [Required]
        [MinLength(2)]
        public string? UpUsername { get; set; }

        public int? UpCoins { get; set; }

        public bool? FirstLogin { get; set; }

        public bool? IsActive {get; set; }
    }
}