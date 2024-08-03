using WeatherApp.Core;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.Infrastructure;

namespace WeatherApp.Application
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _usersRepository;
        private readonly IJWTProvider _provider;

        public UserService(IPasswordHasher passwordHasher,
                           IUserRepository usersRepository, 
                           IJWTProvider provider
                           )
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _provider = provider;
        }
        public async Task Register(string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(email, password);
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result)
            {
                throw new Exception("Failed to login");
            }
            var token = _provider.GenerateToken(user);

            return token;
        }
    }
}
