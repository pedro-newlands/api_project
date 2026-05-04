namespace PokeShop.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionStatus
    {
        Owned, 
        Sold, 
        Transferred

        //Refunded - para lógica de venda sem lucro
    }
}
