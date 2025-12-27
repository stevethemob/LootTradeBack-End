using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class ItemRepository : IItemRepository
    {
        string connString = "";

        public ItemRepository(string connString)
        {
            this.connString = connString;
        }

        public ItemDTO GetItemById(int itemId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string sqlCommand = "SELECT * FROM item WHERE id = @itemId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@itemId", itemId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Item with id " + itemId + " not found");
                    }
                    int id = itemId;
                    int gameId = reader.GetInt32("gameId");
                    string name = reader.GetString("name");
                    string description = reader.GetString("description");

                    return new ItemDTO(id, gameId, name, description);
                }
            }
        }
        public bool CreateItem(string itemName, string ItemDescription)
        {
            return true;
        }

        public List<ItemDTO> GetAllItemsByGameId(int gameId)
        {
            List<ItemDTO> items = new List<ItemDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM item WHERE gameId = @gameId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemDTO item = new ItemDTO
                        (
                        reader.GetInt32("id"),
                        reader.GetInt32("gameId"),
                        reader.GetString("name"),
                        reader.GetString("description")
                        );
                        items.Add(item);
                    }
                }

                return items;
            }
        }
    }
}
