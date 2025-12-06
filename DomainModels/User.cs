using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? RepeatedPassword { get; set; }
        public string Email { get; set; }
        public Enum? role { get; set; }

        public enum Role
        {
            user = 1,
            admin = 2
        }
    }
}
