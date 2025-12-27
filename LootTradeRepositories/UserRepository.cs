using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.CRUD;

namespace LootTradeRepositories
{
    public class UserRepository : IUserRepository
    {
        string connString = "";

        public UserRepository(string connString)
        {
            this.connString = connString;
        }

        public UserDTO GetUserById(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM user WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("user with id " + userId + " does not exist");
                    }

                    int id = reader.GetInt32("id");
                    string username = reader.GetString("username");
                    string password = reader.GetString("password");
                    string email = reader.GetString("email");
                    int roleId = reader.GetInt32("roleId");

                    UserDTO.UserRole role = Enum.IsDefined(typeof(UserDTO.UserRole), roleId)
                        ? (UserDTO.UserRole)roleId
                        : throw new InvalidOperationException("Invalid roleId " + roleId);

                    return new UserDTO(id, username, password, email, role);
                }
            }
        }

        public bool CreateUser(string username, string password, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO user(username, password, email) VALUES(@username, @password, @email)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public int GetUserIdByLogin(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT id FROM user WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No user found");
                    }

                    return reader.GetInt32("id");
                }
            }
        }
    }
}
