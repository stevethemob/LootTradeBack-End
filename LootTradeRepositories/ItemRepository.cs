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
            ItemDTO item = new ItemDTO();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT * FROM item WHERE id = @itemId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@itemId", itemId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item.Id = itemId;
                        item.GameId = reader.GetInt32("gameId");
                        item.Name = reader.GetString("name");
                        item.Description = reader.GetString("description");
                    }
                }
            }

            return item;
        }
        public bool CreateItem(ItemDTO itemDTO)
        {
            return true;
        }
    }
}
