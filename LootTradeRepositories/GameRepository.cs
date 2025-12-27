using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class GameRepository : IGameRepository
    {
        string connString = "";

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

                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GameDTO gameDTO = new GameDTO
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title")
                        };
                        gameDTOs.Add(gameDTO);
                    }
                }

            }
            return gameDTOs;
        }
    }
}
