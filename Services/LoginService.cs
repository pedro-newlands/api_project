using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Services
{
    public class LoginService : ILoginService
    {
        readonly AppDbContext _context;

        public LoginService(AppDbContext context) => _context = context;

        public async Task<User> FirstLogin(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                user = new User
                {
                    UserName = username,
                    PasswordHash = password,
                    Coins = 100,
                    FirstLogin = true
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }

            if (user.PasswordHash != password)
                return null;

            if (!user.FirstLogin)
            {
                user.FirstLogin = true;
                user.Coins += 100;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}