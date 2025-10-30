using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public class LoginService : ILoginService
    {
        readonly AppDbContext _context;

        public LoginService(AppDbContext context) => _context = context;

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                user = new User
                {
                    UserName = username,
                    PasswordHash = password,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }

            if (user.PasswordHash != password)
                throw new ArgumentException("Password is incorrect");

            if (!user.FirstLogin)
            {
                user.FirstLogin = true;
                user.Coins += 100;
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}