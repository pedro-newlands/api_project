using MySqlConnector;

namespace ProjetoPokeShop.Models
{
    public class User
    {
        public int Id { get; set; } // PK

        public string UserName { get; set; }

        public string PasswordHash { get; set; } // senha criptografada

        public int Coins { get; set; } = 0; // moedas iniciais

        public bool FirstLogin { get; set; } = false;

        public ICollection<UserPokemon> Pokeball { get; set; } = new List<UserPokemon>();

    }
}