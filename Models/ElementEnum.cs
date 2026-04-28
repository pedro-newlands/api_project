using System.Text.Json.Serialization;

namespace ProjetoPokeShop.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Elements
    {
        Normal,
        Fire ,
        Water ,
        Electric,
        Grass,
        Ice ,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }
}