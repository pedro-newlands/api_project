using Microsoft.Extensions.Configuration.UserSecrets;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.DTOs
{
    public class SellResultDto
    {
        public string UserName {get; set;}

        public string PokemonName { get; set; }

        public string Nature { get; set; }

        public string Type { get; set; }

        public PokemonRarity Rarity { get; set; }

        public int PokemonMarketValue { get; set; }

        public string CoinsAdjustment { get; set; }
    }
}