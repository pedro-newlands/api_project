using Microsoft.Extensions.Configuration.UserSecrets;

namespace ProjetoPokeShop.DTOs
{
    public class SellResultDto
    {
        public int UserId {get; set;}

        public int PokemonId { get; set; }

        public int PokemonMarketValue { get; set; }
        
        public string CoinsAdjustment { get; set; }
    }
}