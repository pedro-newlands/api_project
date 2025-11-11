using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public interface ILoginService
    {
        Task<User> LoginAsync(string username, string password);
    }
}