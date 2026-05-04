namespace PokeShop.Infra.Repositories
{
    public class LoginRepository : ILoginRepository
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

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserFirstLogin()
        {
            await _context.SaveChangesAsync();
        }
    }
}