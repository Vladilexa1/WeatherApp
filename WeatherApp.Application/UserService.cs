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
        private readonly ILocationRepository _locationRepository;
        public UserService(IPasswordHasher passwordHasher,
                           IUserRepository usersRepository,
                           IJWTProvider provider,
                           ILocationRepository locationRepository)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _provider = provider;
            _locationRepository = locationRepository;
        }
        public async Task Register(string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(email, hashedPassword);
            await _usersRepository.Add(user);
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
            {
                throw new Exception("Failed to login");
            }
            var token = _provider.GenerateToken(user);

            return token;
        }
        public async Task AddLocation(Location location, int userId)
        {
            await _locationRepository.Add(location, userId);
        }
        public async Task DeleteLocation(int idLocation, int idUser)
        {
            await _locationRepository.Delete(idLocation, idUser);
        }
    }
}
