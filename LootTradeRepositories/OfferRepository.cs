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
                string sqlCommand = "INSERT INTO Offered (inventoryId, dateTimeOpen) VALUES (@inventoryId, @dateTime)";
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
                string sqlCommand = "SELECT Offered.id AS offered_id, Offered.dateTimeOpen, Item.id AS item_id, Item.gameId, Item.name, Item.description FROM Offered JOIN Inventory ON Inventory.id = Offered.inventoryId JOIN Item ON Item.id = Inventory.itemId JOIN Game ON Game.id = Item.gameId WHERE Game.id = @gameId AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.OfferedId = Offered.id);";
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
                string sqlCommand = "SELECT Offered.id AS offered_id, Offered.dateTimeOpen, Item.id AS item_id, Item.gameId, Item.name, Item.description FROM Offered JOIN Inventory ON Inventory.id = Offered.inventoryId JOIN Item ON Item.id = Inventory.itemId JOIN Game ON Game.id = Item.gameId WHERE Game.id = @gameId AND Item.name LIKE @searchQuery AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.OfferedId = Offered.id);";
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
                string sqlCommand = "DELETE FROM Offered WHERE id = @offerId;";
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
                string sqlCommand = "SELECT 1 FROM Offered JOIN Inventory ON Offered.inventoryId = Inventory.id WHERE Offered.id = @offerId AND Inventory.userId = @userId LIMIT 1;";
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
                string sqlCommand = "SELECT Offered.id AS offered_id, Offered.dateTimeOpen, Item.id AS item_id, Item.gameId, Item.name, Item.description FROM Offered JOIN Inventory ON Inventory.id = Offered.inventoryId JOIN Item ON Item.id = Inventory.itemId JOIN Game ON Game.id = Item.gameId WHERE Inventory.userId = @userId AND Game.id = @gameId AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.OfferedId = Offered.id);";
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
                string sqlCommand = "SELECT Offered.id AS offered_id, Offered.dateTimeOpen, Item.id AS item_id, Item.gameId, Item.name, Item.description FROM Offered JOIN Inventory ON Inventory.id = Offered.inventoryId JOIN Item ON Item.id = Inventory.itemId JOIN Game ON Game.id = Item.gameId WHERE Offered.id = @offerId;";
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
