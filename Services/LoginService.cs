using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Repositories;

namespace ProjetoPokeShop.Services
{
    public class LoginService : ILoginService
    {
        readonly LoginRepository _repository;

        public LoginService(LoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _repository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                user = new User
                {
                    UserName = username,
                    PasswordHash = password,
                    Coins = 100,
                    FirstLogin = true
                };

                await _repository.CreateUserAsync(user);
            }

            if (user.PasswordHash != password)
                throw new ArgumentException("Password is incorrect");

            if (!user.FirstLogin)
            {
                user.FirstLogin = true;
<<<<<<< HEAD
                user.Coins += 100;
=======
                
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                await _repository.UpdateUserFirstLogin(user);
            }
            return user;
        }
    }
} 