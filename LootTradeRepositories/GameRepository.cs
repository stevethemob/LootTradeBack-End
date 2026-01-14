using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class GameRepository : IGameRepository
    {
        readonly string connString;

        public GameRepository(string connString)
        {
            this.connString = connString;
        }

        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> gameDTOs = new List<GameDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM game";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GameDTO gameDTO = new GameDTO
                        (
                            reader.GetInt32("id"),
                            reader.GetString("title")
                        );
                        gameDTOs.Add(gameDTO);
                    }
                }

            }
            return gameDTOs;
        }

        public bool AddGame(string gameTitle)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO game(title) VALUES(@gameTitle);";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameTitle", gameTitle);

                cmd.ExecuteNonQuery();
            }

            return false;
        }

        public bool GameExistsWithTitle(string gameTitle)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT title FROM game WHERE title = @gameTitle";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameTitle", gameTitle);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public bool EditGameWithGameId(int gameId, string gameTitle)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "UPDATE game SET title= @gameTitle WHERE id = @gameId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                cmd.Parameters.AddWithValue("@gameTitle", gameTitle);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public GameDTO GetGameByGameId(int gameId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM game WHERE id = @gameId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Item with id " + gameId + " not found");
                    }

                    string gameTitle = reader.GetString("title");

                    return new GameDTO(gameId, gameTitle);
                }
            }
        }
    }
}
