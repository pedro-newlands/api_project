using PokeShop.Application.DTOs.Login;

namespace PokeShop.Application.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResultDto> LoginAsync(string username, string password);
    }
}