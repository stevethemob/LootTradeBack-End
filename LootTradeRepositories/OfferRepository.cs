using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class OfferRepository : IOfferRepository
    {
        string connString = "";

        public OfferRepository(string connString)
        {
            this.connString = connString;
        }

        public bool AddOffer(int inventoryId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sqlCommand = "INSERT INTO Offer (inventoryId, dateTimeOpen) VALUES(@inventoryId, @dateTime)";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                cmd.ExecuteNonQuery();
            }

            return true;
        }
    }
}
