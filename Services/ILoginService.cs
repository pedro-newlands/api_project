using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);
    }
}