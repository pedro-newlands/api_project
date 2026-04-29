using System.Text.Json.Serialization;

namespace ProjetoPokeShop.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Rarities
    {
        Common, 
        Uncommon, 
        Rare, 
        Legendary 
    }
}