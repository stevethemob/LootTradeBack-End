using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly string connString;

        private static class Params
        {
            public const string TradeId = "@tradeId";
            public const string OfferedId = "@offeredId";
            public const string UserId = "@userId";
            public const string GameId = "@gameId";
            public const string ItemId = "@itemId";
            public const string InventoryId = "@inventoryId";
            public const string TraderId = "@traderId";
            public const string OfferId = "@offerId";
        }

        private static class Columns
        {
            public const string Id = "id";
            public const string OfferedId = "offeredId";
            public const string Username = "username";
            public const string GameId = "gameId";
            public const string Name = "name";
            public const string Description = "description";

            public const string OfferItemId = "offerItemId";
            public const string OfferGameId = "offerGameId";
            public const string OfferItemName = "offerItemName";
            public const string OfferItemDescription = "offerItemDescription";
            public const string TradeUsername = "tradeUsername";
        }

        public TradeRepository(string connString)
        {
            this.connString = connString;
        }

        public bool AddTradeOffer(int offerId, List<int> itemIds, int traderId)
        {
            int tradeId = AddTradeTableRowAndReturnId(offerId);

            foreach (int itemId in itemIds)
            {
                int inventoryId = GetInventoryIdByItemAndTraderId(itemId, traderId);
                AddItemsToTrade(tradeId, inventoryId);
            }

            return true;
        }

        private int AddTradeTableRowAndReturnId(int offerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO trade (offeredId) VALUES (@offerId)";
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.AddWithValue(Params.OfferId, offerId);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        private void AddItemsToTrade(int tradeId, int inventoryId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO trade_item (tradeId, inventoryId) VALUES (@tradeId, @inventoryId)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);
                cmd.Parameters.AddWithValue(Params.InventoryId, inventoryId);
                cmd.ExecuteNonQuery();
            }
        }

        private int GetInventoryIdByItemAndTraderId(int itemId, int traderId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT id FROM inventory WHERE itemId = @itemId AND userId = @traderId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.ItemId, itemId);
                cmd.Parameters.AddWithValue(Params.TraderId, traderId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("Inventory item not found");

                    return reader.GetInt32(Columns.Id);
                }
            }
        }

        public AllTradesDTO GetAllTradeIdsByGameIdAndUserId(int gameId, int userId)
        {
            AllTradesDTO allTrades = new AllTradesDTO();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT DISTINCT t.id AS tradeId, bidder.username FROM trade t JOIN trade_item ti ON ti.tradeId = t.id JOIN inventory bidInv ON ti.inventoryId = bidInv.id JOIN user bidder ON bidInv.userId = bidder.id JOIN offered o ON t.offeredId = o.id JOIN inventory offerInv ON o.inventoryId = offerInv.id JOIN item offerItem ON offerInv.itemId = offerItem.id JOIN game g ON offerItem.gameId = g.id WHERE offerInv.userId = @userId AND bidInv.userId != @userId AND g.id = @gameId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.GameId, gameId);
                cmd.Parameters.AddWithValue(Params.UserId, userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allTrades.TradeIds.Add(reader.GetInt32(Columns.Id));
                        allTrades.TraderUsernames.Add(reader.GetString(Columns.Username));
                    }
                }
            }

            return allTrades;
        }

        public TradeDTO GetTradeByTradeId(int tradeId)
        {
            TradeDTO trade = GetTradeWithoutItemsByTradeId(tradeId);
            foreach (ItemDTO item in GetAllItemsInOneTradeByTradeId(tradeId))
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
                string sqlCommand = "SELECT trade.id AS tradeId, item.id AS offerItemId, item.gameId AS offerGameId, item.name AS offerItemName, item.description AS offerItemDescription, user.username AS tradeUsername FROM trade JOIN offered ON trade.offeredId = offered.id JOIN inventory ON offered.inventoryId = inventory.id JOIN item ON inventory.itemId = item.id JOIN user ON inventory.userId = user.id WHERE trade.id = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException($"Trade with id {tradeId} not found");

                    ItemDTO item = new ItemDTO(
                        reader.GetInt32(Columns.OfferItemId),
                        reader.GetInt32(Columns.OfferGameId),
                        reader.GetString(Columns.OfferItemName),
                        reader.GetString(Columns.OfferItemDescription)
                    );

                    return new TradeDTO(tradeId, item, reader.GetString(Columns.TradeUsername));
                }
            }
        }

        private List<ItemDTO> GetAllItemsInOneTradeByTradeId(int tradeId)
        {
            List<ItemDTO> items = new List<ItemDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT item.id, item.gameId, item.name, item.description FROM trade_item JOIN inventory ON trade_item.inventoryId = inventory.id JOIN item ON inventory.itemId = item.id WHERE trade_item.tradeId = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemDTO(
                            reader.GetInt32(Columns.Id),
                            reader.GetInt32(Columns.GameId),
                            reader.GetString(Columns.Name),
                            reader.GetString(Columns.Description)
                        ));
                    }
                }
            }

            return items;
        }

        public bool AcceptTrade(int tradeId)
        {
            int offeredId = GetOfferedIdByTradeId(tradeId);
            if (CheckIfTradeIsAlreadyAccepted(offeredId))
                return false;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO accepted_trade (tradeId, offeredId) VALUES (@tradeId, @offeredId)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);
                cmd.Parameters.AddWithValue(Params.OfferedId, offeredId);
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        private int GetOfferedIdByTradeId(int tradeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offeredId FROM trade WHERE id = @tradeId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException($"Trade with id {tradeId} not found");

                    return reader.GetInt32(Columns.OfferedId);
                }
            }
        }

        private bool CheckIfTradeIsAlreadyAccepted(int offeredId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT id FROM accepted_trade WHERE offeredId = @offeredId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.OfferedId, offeredId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public bool CheckIfTradeIsBySameUser(int tradeId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT trade.id FROM trade JOIN offered ON offered.id = trade.offeredId JOIN inventory ON inventory.id = offered.inventoryId WHERE trade.id = @tradeId AND inventory.userId = @userId";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.TradeId, tradeId);
                cmd.Parameters.AddWithValue(Params.UserId, userId);

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

        public List<TradeAdminDTO> GetAllTradesByGameId(int gameId)
        {
            List<TradeAdminDTO> trades = new List<TradeAdminDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT DISTINCT trade.id AS tradeId, offerer.username AS offererName, trader.username AS traderName FROM trade JOIN offered ON trade.offeredId = offered.id JOIN inventory offerInv ON offered.inventoryId = offerInv.id JOIN user offerer ON offerInv.userId = offerer.id JOIN trade_item ti ON ti.tradeId = trade.id JOIN inventory tradeInv ON ti.inventoryId = tradeInv.id JOIN user trader ON tradeInv.userId = trader.id JOIN item ON offerInv.itemId = item.id JOIN game ON item.gameId = game.id WHERE game.id = @gameId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue(Params.GameId, gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trades.Add(new TradeAdminDTO(
                            reader.GetInt32("tradeId"),
                            reader.GetString("offererName"),
                            reader.GetString("traderName")
                        ));
                    }
                }
            }

            return trades;
        }
    }
}
