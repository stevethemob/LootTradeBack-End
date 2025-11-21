using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeRepositories
{
    public class InventoryRepository : IInventoryRepository
    {
        string connString = "";

        public InventoryRepository(string connString)
        {
            this.connString = connString;
        }

        public InventoryDTO GetInventoryByUserIdAndGameId(int userId, int gameId)
        {
            InventoryDTO inventory = new InventoryDTO();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT item.* FROM item JOIN Inventory ON inventory.itemId = item.id JOIN User ON user.id = inventory.userId WHERE user.id = @userId AND item.gameId = @gameId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemDTO item = new ItemDTO();
                        item.Id = reader.GetInt32("id");
                        item.GameId = reader.GetInt32("gameId");
                        item.Name = reader.GetString("name");
                        item.Description = reader.GetString("description");
                        inventory.Items.Add(item);
                    }
                }

                return inventory;
            }
        }
    }
}
