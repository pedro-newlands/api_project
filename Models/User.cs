namespace ProjetoPokeShop.Models
{
    public class User
    {
        public int Id { get; set; } // PK

        public string UserName { get; set; }

        public string PasswordHash { get; set; } // senha criptografada

        public int Coins { get; set; } = 0; // moedas iniciais

        public bool FirstLogin { get; set; } = false;

<<<<<<< HEAD
        // public ICollection<UserPokemon> Pokeball { get; set; } = new List<UserPokemon>(); --> redundante
=======
        public bool IsActive {get; set; } = true; //permite o DeleteBehavior.Restrict apropriado e não quebra o histórico de registros das transações
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

    }
}