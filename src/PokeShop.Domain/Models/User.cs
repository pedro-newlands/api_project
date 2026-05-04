namespace PokeShop.Domain.Models
{
    public class User
    {
        public int Id { get; set; } // PK

        public string UserName { get; set; }

        public string PasswordHash { get; set; } // senha criptografada

        public int Coins { get; set; } = 0; // moedas iniciais

        public bool FirstLogin { get; set; } = false;

        public bool IsActive {get; set; } = true; //permite o DeleteBehavior.Restrict apropriado e não quebra o histórico de registros das transações

    }
}