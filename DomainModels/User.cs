using LootTradeDTOs;

namespace LootTradeDomainModels
{
    public class User
    {
        public User(string username, string password, string email, UserRole role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;

        }

        public User(int id, string username, string password, string email, UserRole role)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;
            Role = role;
        }

        public User(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Username = userDTO.Username;
            Password = userDTO.Password;
            Email = userDTO.Email;
            Role = MapRole(userDTO.Role);
        }

        private static UserRole MapRole(UserDTO.UserRole DTORole)
        {
            return DTORole switch
            {
                UserDTO.UserRole.User => UserRole.User,
                UserDTO.UserRole.Admin => UserRole.Admin,
                _ => throw new ArgumentOutOfRangeException(nameof(DTORole), DTORole, "Invalid role")
            };
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
