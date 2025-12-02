using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Repositories
{
    public class LoginRepository
    {
        readonly AppDbContext _context;

        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserFirstLogin(User user)
        {
            user.FirstLogin = true;
            user.Coins += 100;
            await _context.SaveChangesAsync();
        }
    }
}