using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

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
    }
}
