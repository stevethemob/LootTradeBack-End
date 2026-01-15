using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public class TradeTest
    {
        [TestMethod]
        public void AddTradeOfferTestTrue()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int offerId = 1;
            List<int> itemIds = new List<int>();
            int traderId = 1;
            itemIds.Add(1);

            bool success = tradeService.AddTradeOffer(offerId, itemIds, traderId);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void AddTradeOfferTestFalse()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int offerId = 1;
            List<int> itemIds = new List<int>();
            int traderId = 1;

            bool success = tradeService.AddTradeOffer(offerId, itemIds, traderId);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void GetAllTradeIdsByGameIdAndUserIdTest()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int gameId = 1;
            int userId = 1;

            AllTrades trades = tradeService.GetAllTradeIdsByGameIdAndUserId(gameId, userId);

            Assert.AreEqual(2, trades.TradeIds.Count());
        }

        [TestMethod]
        public void GetTradeByTradeIdTest()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int tradeId = 1;

            Trade trade = tradeService.GetTradeByTradeId(tradeId);

            Assert.IsNotNull(trade);
        }

        [TestMethod]
        public void AcceptTradeTest()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int tradeId = 1;
            int userId = 1;

            bool success = tradeService.AcceptTrade(tradeId, userId);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetAllTradesByGameId()
        {
            TradeRepositoryMock tradeRepositoryMock = new TradeRepositoryMock();
            TradeService tradeService = new TradeService(tradeRepositoryMock);

            int gameId = 1;

            List<TradeAdmin> trades = tradeService.GetAllTradesByGameId(gameId);

            Assert.AreEqual(1, trades.Count());
        }
    }
}