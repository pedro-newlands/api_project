using System.Text.Json.Serialization;

namespace ProjetoPokeShop.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PokemonRarity
    {
        Common, 
        Uncommon, 
        Rare, 
        Legendary 
    }
}