using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace LootTradeRepositories
{
    public class OfferRepository : IOfferRepository
    {
        readonly string connString;

        public OfferRepository(string connString)
        {
            this.connString = connString;
        }

        public bool AddOffer(int inventoryId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO Offered (inventoryId, dateTimeOpen) VALUES(@inventoryId, @dateTime)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public List<OfferDTO> GetAllOffersByGameId(int gameId)
        {
            List<OfferDTO> offers = new List<OfferDTO>();

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
                        OfferDTO offer = new OfferDTO();
                        offer.Id = reader.GetInt32("offered_id");
                        offer.DateTimeOpen = reader.GetDateTime("dateTimeOpen");
                        offer.Item = new ItemDTO(
                        reader.GetInt32("item_id"),
                        reader.GetInt32("gameId"),
                        reader.GetString("name"),
                        reader.GetString("description")
                        );
                        offers.Add(offer);
                    }
                }
            }

            return offers;
        }

        public List<OfferDTO> GetOffersBySearchAndGameId(string searchQuery, int gameId)
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "SELECT Offered.id AS offered_id, Offered.dateTimeOpen, Item.id AS item_id, Item.name, Item.description FROM Offered JOIN Inventory ON Inventory.id = Offered.inventoryId JOIN Item ON Item.id = Inventory.itemId JOIN Game ON Game.id = Item.gameId WHERE Game.id = @gameId AND item.name LIKE @searchQuery AND NOT EXISTS (SELECT 1 FROM accepted_trade at WHERE at.OfferedId = Offered.id);";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                cmd.Parameters.AddWithValue("@gameId", gameId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OfferDTO offer = new OfferDTO();
                        offer.Id = reader.GetInt32("offered_id");
                        offer.DateTimeOpen = reader.GetDateTime("dateTimeOpen");
                        offer.Item = new ItemDTO(
                        reader.GetInt32("item_id"),
                        gameId,
                        reader.GetString("name"),
                        reader.GetString("description")
                        );
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
                string sqlCommand = "SELECT 1 FROM offered JOIN inventory ON offered.inventoryId = inventory.id WHERE offered.id = @offerId AND inventory.userId = @userId LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@offerId", offerId);

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
