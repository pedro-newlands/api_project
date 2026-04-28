using System.Text.Json.Serialization;

namespace ProjetoPokeShop.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
<<<<<<< HEAD
    public enum PokemonRarity
=======
    public enum Rarities
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
    {
        Common, 
        Uncommon, 
        Rare, 
        Legendary 
    }
}