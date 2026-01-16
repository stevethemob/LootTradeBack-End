using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly string connString;

        private static class Columns
        {
            public const string OfferedId = "offered_id";
            public const string DateTimeOpen = "dateTimeOpen";
            public const string ItemId = "item_id";
            public const string GameId = "gameId";
            public const string Name = "name";
            public const string Description = "description";
        }

        public OfferRepository(string connString)
        {
            this.connString = connString;
        }

        public bool AddOffer(int inventoryId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO offered (inventoryId, dateTimeOpen) VALUES (@inventoryId, @dateTime)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public List<OfferDTO> GetAllOffersByGameId(int gameId)
        {
            List<OfferDTO> offers = new();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offered.id AS offered_id, offered.dateTimeOpen, item.id AS item_id, item.gameId, item.name, item.description FROM offered JOIN inventory ON inventory.id = offered.inventoryId JOIN item ON item.id = inventory.itemId JOIN game ON game.id = item.gameId WHERE game.id = @gameId AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.offeredId = offered.id);";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        offers.Add(ReadOffer(reader));
                    }
                }
            }

            return offers;
        }

        public List<OfferDTO> GetOffersBySearchAndGameId(string searchQuery, int gameId)
        {
            List<OfferDTO> offers = new();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offered.id AS offered_id, offered.dateTimeOpen, item.id AS item_id, item.gameId, item.name, item.description FROM offered JOIN inventory ON inventory.id = offered.inventoryId JOIN item ON item.id = inventory.itemId JOIN game ON game.id = item.gameId WHERE game.id = @gameId AND item.name LIKE @searchQuery AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.offeredId = offered.id);";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@searchQuery", $"%{searchQuery}%");
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OfferDTO offer = ReadOffer(reader);
                        offer.Item.GameId = gameId;
                        offers.Add(offer);
                    }
                }
            }

            return offers;
        }

        public bool DeleteOfferById(int offerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "DELETE FROM offered WHERE id = @offerId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@offerId", offerId);
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public bool CheckIfOfferIsBySameUser(int userId, int offerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT 1 FROM offered JOIN inventory ON offered.inventoryId = inventory.id WHERE offered.id = @offerId AND inventory.userId = @userId LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@offerId", offerId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public List<OfferDTO> GetAllOffersOfSpecificUserByUserIdAndGameId(int userId, int gameId)
        {
            List<OfferDTO> offers = new();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offered.id AS offered_id, offered.dateTimeOpen, item.id AS item_id, item.gameId, item.name, item.description FROM offered JOIN inventory ON inventory.id = offered.inventoryId JOIN item ON item.id = inventory.itemId JOIN game ON game.id = item.gameId WHERE inventory.userId = @userId AND game.id = @gameId AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.offeredId = offered.id);";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        offers.Add(ReadOffer(reader));
                    }
                }
            }

            return offers;
        }

        public OfferDTO GetOfferDetailsByOfferId(int offerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT offered.id AS offered_id, offered.dateTimeOpen, item.id AS item_id, item.gameId, item.name, item.description FROM offered JOIN inventory ON inventory.id = offered.inventoryId JOIN item ON item.id = inventory.itemId JOIN game ON game.id = Item.gameId WHERE offered.id = @offerId;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@offerId", offerId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("No offer found");

                    return ReadOffer(reader);
                }
            }
        }

        private static OfferDTO ReadOffer(MySqlDataReader reader)
        {
            return new OfferDTO
            {
                Id = reader.GetInt32(Columns.OfferedId),
                DateTimeOpen = reader.GetDateTime(Columns.DateTimeOpen),
                Item = new ItemDTO(
                    reader.GetInt32(Columns.ItemId),
                    reader.GetInt32(Columns.GameId),
                    reader.GetString(Columns.Name),
                    reader.GetString(Columns.Description))
            };
        }
    }
}
