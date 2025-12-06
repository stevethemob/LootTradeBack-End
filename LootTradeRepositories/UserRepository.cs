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
            UserDTO userDTO = new UserDTO();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM user WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDTO.Id = reader.GetInt32("id");
                        userDTO.Username = reader.GetString("username");
                        userDTO.Password = reader.GetString("password");
                        userDTO.Email = reader.GetString("email");
                        int roleId = reader.GetInt32("roleId");
                    }
                }

                return userDTO;
            }
        }

        public void CreateUser(UserDTO user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO user(username, password, email) VALUES(@username, @password, @email)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);

                cmd.ExecuteNonQuery();
            }
        }

        public int GetUserIdByLogin(UserDTO user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT id FROM user WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = reader.GetInt32("id");
                    }
                }
            }

            return user.Id;
        }
    }
}
