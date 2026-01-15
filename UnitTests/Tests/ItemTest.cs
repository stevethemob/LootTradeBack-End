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

            int gameId = 1;
            string itemName = "Test";
            string itemDescription = "Test";

            bool success = itemService.CreateItem(gameId, itemName, itemDescription);

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

        [TestMethod]
        public void EditItemTest1()
        {
            ItemRepositoryMock itemRepositoryMock = new ItemRepositoryMock();
            ItemService itemService = new ItemService(itemRepositoryMock);

            int itemId = 1;
            int gameId = 1;
            string name = "Test";
            string description = "Test";

            Item item = new Item(itemId, gameId, name, description);


            bool success = itemService.EditItem(item);

            Assert.IsTrue(success);
        }
    }
}
