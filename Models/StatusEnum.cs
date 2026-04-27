using System.Text.Json.Serialization;

namespace ProjetoPokeShop.Models
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
