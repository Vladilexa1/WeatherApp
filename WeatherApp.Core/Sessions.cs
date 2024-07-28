using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Sessions
    {
        private Sessions(Guid id, int userId, DateTime expiresAt)
        {
            Id = id;
            UserId = userId;
            ExpiresAt = expiresAt;
        }
        public Guid Id { get; }
        public int UserId { get; }
        public DateTime ExpiresAt { get; }
        public static Sessions Create(Guid id, int userId, DateTime expiresAt) // TODO: validation
        {
            var Sessions = new Sessions(id, userId, expiresAt);
            return Sessions;
        }
    }
}
