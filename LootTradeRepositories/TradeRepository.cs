using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeRepositories
{
    public class TradeRepository : ITradeRepository
    {
        readonly string connString;

        public TradeRepository(string connString)
        {
            this.connString = connString;
        }

        public bool AddTradeOffer(int offerId, List<int> itemIds, int traderId)
        {
            int TradeId = AddTradeTableRowAndReturnId(offerId);

            foreach (int itemId in itemIds)
            {
                int inventoryId = GetInventoryIdByItemAndTraderId(itemId, traderId);
                AddItemsToTrade(TradeId, inventoryId);
            }

            return true;
        }

        private int AddTradeTableRowAndReturnId(int offerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO trade(offeredId) VALUES (@offerId)";
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@offerId", offerId);

                    cmd.ExecuteNonQuery();

                    return (int)cmd.LastInsertedId;
                }
            }
        }

        private bool AddItemsToTrade(int tradeId, int inventoryId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO trade_item (tradeId, inventoryId) VALUES (@tradeId, @InventoryId)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);
                cmd.Parameters.AddWithValue("@inventoryId", inventoryId);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        private int GetInventoryIdByItemAndTraderId(int itemId, int traderId)
        {
            int id = 0;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT id FROM inventory WHERE itemId = @itemId AND userId = @traderId";
                MySqlCommand cmd = new MySqlCommand( sqlCommand, conn);
                cmd.Parameters.AddWithValue("@itemId", itemId);
                cmd.Parameters.AddWithValue("@traderId", traderId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = reader.GetInt32("id");
                    }
                }
            }

            return id;
        }
    }
}
