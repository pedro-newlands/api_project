namespace PokeShop.Domain.Enums
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