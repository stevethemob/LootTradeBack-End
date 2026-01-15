using LootTradeDTOs;
using LootTradeInterfaces;

namespace UnitTests.MockRepositories
{
    public class TradeRepositoryMock : ITradeRepository
    {
        public bool AddTradeOffer(int offerId, List<int> itemIds, int traderId)
        {
            return true;
        }

        public AllTradesDTO GetAllTradeIdsByGameIdAndUserId(int gameId, int userId)
        {
            AllTradesDTO trades = new AllTradesDTO();

            trades.TradeIds.Add(1);
            trades.TraderUsernames.Add("test");
            trades.TradeIds.Add(2);
            trades.TraderUsernames.Add("test2");

            return trades;
        }

        public TradeDTO GetTradeByTradeId(int tradeId)
        {
            ItemDTO item = new ItemDTO(1, 1, "test", "test");

            TradeDTO trade = new TradeDTO(1, item, "test");

            return trade;
        }

        public bool AcceptTrade(int tradeId)
        {
            return true;
        }

        public bool CheckIfTradeIsBySameUser(int tradeId, int userId)
        {
            return false;
        }

        public List<TradeAdminDTO> GetAllTradesByGameId(int gameId)
        {
            List<TradeAdminDTO> trades = new List<TradeAdminDTO>();

            TradeAdminDTO trade = new TradeAdminDTO(1, "test", "test");
            trades.Add(trade);

            return trades;
        }
    }
}
