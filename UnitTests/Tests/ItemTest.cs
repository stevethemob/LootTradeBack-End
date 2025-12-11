using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void GetItemByIdTest()
        { 
            ItemRepositoryMock itemRepositoryMock = new ItemRepositoryMock();
            ItemService itemService = new ItemService(itemRepositoryMock);

            int itemId = 1;

            Item item = itemService.GetItemById(itemId);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void CreateItemTest()
        {
            ItemRepositoryMock itemRepositoryMock = new ItemRepositoryMock();
            ItemService itemService = new ItemService(itemRepositoryMock);

            Item item = new Item();
            item.Name = "Test";
            item.Description = "Test";
            item.GameId = 1;

            bool success = itemService.CreateItem(item);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetAllItemsByGameIdTest()
        {
            ItemRepositoryMock itemRepositoryMock = new ItemRepositoryMock();
            ItemService itemService = new ItemService(itemRepositoryMock);

            int gameId = 1;

            List<Item> items = itemService.GetAllItemsByGameId(gameId);

            Assert.IsNotNull(items);
        }
    }
}
