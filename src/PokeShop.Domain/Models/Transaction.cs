namespace PokeShop.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; } 

        public int? PokemonId { get; set; }

        public Pokemon? Pokemon { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public TransactionStatus Status { get; set; }

        public string CoinsAdjustment { get; set; }
    }
}