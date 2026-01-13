using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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

        public AllTradesDTO GetAllTradeIdsByGameIdAndUserId(int gameId, int userId)
        {
            AllTradesDTO AllTrades = new AllTradesDTO();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT Trade.id, User.username FROM Trade JOIN Offered ON Trade.offeredId = Offered.id JOIN Inventory ON Offered.inventoryId = Inventory.id JOIN Item ON Inventory.itemId = Item.id JOIN Game ON Item.gameId = Game.id JOIN User ON Inventory.userId = User.id WHERE Inventory.userId = @userId AND Game.id = @gameId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AllTrades.TradeIds.Add(reader.GetInt32("id"));
                        AllTrades.TraderUsernames.Add(reader.GetString("username"));
                    }
                }
            }

            return AllTrades;
        }

        public TradeDTO GetTradeByTradeId(int tradeId)
        {
            TradeDTO trade = GetTradeWithoutItemsByTradeId(tradeId);
            List<ItemDTO> items = GetAllItemsInOneTradeByTradeId(tradeId);
            foreach (ItemDTO item in items)
            {
                trade.TradeOffers.Add(item);
            }

            return trade;
        }

        private TradeDTO GetTradeWithoutItemsByTradeId(int tradeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT Trade.id AS tradeId, Item.id AS offerItemId, Item.gameId AS offerGameId, Item.name AS offerItemName, Item.description AS offerItemDescription, User.username AS tradeUsername FROM Trade JOIN Offered ON Trade.offeredId = Offered.id JOIN Inventory ON Offered.inventoryId = Inventory.id JOIN Item ON Inventory.itemId = Item.id JOIN User ON Inventory.userId = User.id WHERE Trade.id = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Item with id " + tradeId + " not found");
                    }
                    int offerItemId = reader.GetInt32("offerItemId");
                    int offerItemGameId = reader.GetInt32("offerGameId");
                    string offerItemName = reader.GetString("offerItemName");
                    string offerItemDescription = reader.GetString("offerItemDescription");
                    string tradeUsername = reader.GetString("tradeUsername");

                    ItemDTO item = new ItemDTO(offerItemId, offerItemGameId, offerItemName, offerItemDescription);

                    return new TradeDTO(tradeId, item, tradeUsername);
                }
            }
        }

        private List<ItemDTO> GetAllItemsInOneTradeByTradeId(int tradeId)
        {
            List<ItemDTO> items = new List<ItemDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT Item.id, Item.gameId, Item.name, Item.description FROM Trade_Item JOIN Inventory ON Trade_Item.inventoryId = Inventory.id JOIN Item ON Inventory.itemId = Item.id WHERE Trade_Item.tradeId = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int gameId = reader.GetInt32("gameId");
                        string itemName = reader.GetString("name");
                        string description = reader.GetString("description");
                        ItemDTO item = new ItemDTO(id, gameId, itemName, description);
                        items.Add(item);
                    }
                }
            }


            return items;
        }

        public bool AcceptTrade(int tradeId)
        { 
            int offeredId = GetOfferedIdByTradeId(tradeId);
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO accepted_trade(tradeId, offeredId) VALUES(@tradeId, @offeredId)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);
                cmd.Parameters.AddWithValue("@offeredId", offeredId);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        private int GetOfferedIdByTradeId(int tradeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offeredId FROM trade WHERE Id = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);

                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Item with id " + tradeId + " not found");
                    }
                    int id = reader.GetInt32("offeredId");

                    return id;
                }
            }
        }

        public bool CheckIfTradeIsBySameUser(int tradeId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT 1 FROM trade JOIN offered ON offered.id = trade.offeredId JOIN inventory ON inventory.id = offered.inventoryId WHERE trade.id = @tradeId AND inventory.userId = @userId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@tradeId", tradeId);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    return true;
                }
            }
        }
    }
}
