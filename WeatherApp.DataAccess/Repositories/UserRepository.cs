using Microsoft.EntityFrameworkCore;
using WeatherApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess.Entitys;

namespace WeatherApp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WeatherAppDbContext _context;
        public UserRepository(WeatherAppDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                PasswordHash = user.PasswordHash
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == email)
                ?? throw new Exception();

            var user = User.Create(userEntity.Login, userEntity.PasswordHash);

            return user;
        }
    }
}
