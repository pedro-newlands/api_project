using PokeShop.Application.DTOs.Login;

namespace PokeShop.Application.Services
{
    public class LoginService : ILoginService
    {
        readonly ILoginRepository _repository;

        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResultDto> LoginAsync(string username, string password)
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
                user.Coins += 100;

                await _repository.UpdateUserFirstLogin();
            }
            
            return new LoginResultDto(
                "Login succeed",
                user.UserName,
                user.Coins
            );
        }
    }
} 