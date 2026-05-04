namespace PokeShop.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<User?> GetUserByUserNameAsync(string username);
        Task CreateUserAsync(User user);
        Task UpdateUserFirstLogin();
    }
}
     