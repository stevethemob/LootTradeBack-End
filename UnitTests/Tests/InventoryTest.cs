using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace UnitTests.Tests
{
    [TestClass]
    public class InventoryTest
    {
        [TestMethod]
        public void GetInventoryByUserIdAndGameId()
        {
            InventoryRepositoryMock repository = new InventoryRepositoryMock();
            InventoryService inventoryService = new InventoryService(repository);
            int userId = 1;
            int gameId = 1;

            Inventory inventory = inventoryService.GetInventoryByUserIdAndGameId(userId, gameId);

            Assert.AreEqual(1, inventory.Items.Count);
        }

        [TestMethod]
        public void AddItemToUserTheirInventoryByUserIdAndItemId()
        {
            InventoryRepositoryMock repository = new InventoryRepositoryMock();
            InventoryService inventoryService = new InventoryService(repository);
            int userId = 1;
            int itemId = 1;

            bool succeed = inventoryService.AddItemToUserTheirInventoryByUserIdAndItemId(userId, itemId);

            Assert.IsTrue(succeed);
        }
    }
}
