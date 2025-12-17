using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public class OfferTest
    {
        [TestMethod]
        public void AddOfferTestTrue()
        {
            OfferRepositoryMock offerRepository = new OfferRepositoryMock();
            InventoryRepositoryMock inventoryRepository = new InventoryRepositoryMock();

            OfferService offerService = new OfferService(offerRepository, inventoryRepository);
            int userId = 1;
            int itemId = 1;

            bool success = offerService.AddOffer(userId, itemId);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetAllOffersByGameIdCount2()
        {
            OfferRepositoryMock offerRepository = new OfferRepositoryMock();
            InventoryRepositoryMock inventoryRepository = new InventoryRepositoryMock();

            OfferService offerService = new OfferService(offerRepository, inventoryRepository);

            int gameId = 1;

            List<Offer> offers = offerService.GetAllOffersByGameId(gameId);

            Assert.AreEqual(offers.Count, 2);
        }
    }
}
