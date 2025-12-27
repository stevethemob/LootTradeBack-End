using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class UserDTO
    {
        public UserDTO(string username, string password, string email, UserRole role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            
        }
        public UserDTO(int id, string username, string password, string email, UserRole role)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            Role = role;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        public enum UserRole
        {
            User = 1,
            Admin = 2
        }
    }
}
