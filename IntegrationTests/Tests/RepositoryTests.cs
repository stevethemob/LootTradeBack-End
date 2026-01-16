using LootTradeRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LootTrade.IntegrationTests.TestFixtures;
using LootTradeDTOs;

namespace IntegrationTests.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private static MySqlDatabaseFixture? fixture;

        private static GameRepository? gameRepo;
        private static ItemRepository? itemRepo;
        private static InventoryRepository? inventoryRepo;
        private static OfferRepository? offerRepo;
        private static TradeRepository? tradeRepo;
        private static UserRepository? userRepo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            fixture = new MySqlDatabaseFixture();
            gameRepo = new GameRepository(fixture.ConnectionString);
            itemRepo = new ItemRepository(fixture.ConnectionString);
            inventoryRepo = new InventoryRepository(fixture.ConnectionString);
            offerRepo = new OfferRepository(fixture.ConnectionString);
            tradeRepo = new TradeRepository(fixture.ConnectionString);
            userRepo = new UserRepository(fixture.ConnectionString);
        }

        [TestMethod]
        public void GetAllGames_ReturnsSeededGames()
        {
            List<GameDTO> games = gameRepo!.GetAllGames();

            Assert.IsTrue(games.Exists(g => g.Title == "Game A"));
            Assert.IsTrue(games.Exists(g => g.Title == "Game B"));
        }

        [TestMethod]
        public void AddGame_WorksAndExists()
        {
            string newGameTitle = "Game C";
            gameRepo!.AddGame(newGameTitle);
            Assert.IsTrue(gameRepo.GameExistsWithTitle(newGameTitle));
        }

        [TestMethod]
        public void EditGame_UpdatesTitle_Isolated()
        {
            string originalTitle = "Game D";
            gameRepo!.AddGame(originalTitle);

            GameDTO game = gameRepo.GetAllGames().Find(g => g.Title == originalTitle)!;

            string newTitle = originalTitle + "_Edited";
            bool edited = gameRepo.EditGameWithGameId(game.Id, newTitle);
            Assert.IsTrue(edited);

            GameDTO updatedGame = gameRepo.GetGameByGameId(game.Id);
            Assert.AreEqual(newTitle, updatedGame.Title);
        }

        [TestMethod]
        public void GetInventoryByUserIdAndGameId_ReturnsSeededItems()
        {
            InventoryDTO inventory = inventoryRepo!.GetInventoryByUserIdAndGameId(1, 1);

            Assert.IsTrue(inventory.Items.Any(i => i.Name == "Sword"));
            Assert.IsTrue(inventory.Items.Any(i => i.Name == "Shield"));
        }

        [TestMethod]
        public void AddItemToUserTheirInventoryByUserIdAndItemId_AddsNewItem()
        {
            bool added = inventoryRepo!.AddItemToUserTheirInventoryByUserIdAndItemId(1, 3);
            Assert.IsTrue(added);

            InventoryDTO inventory = inventoryRepo.GetInventoryByUserIdAndGameId(1, 2);
            Assert.IsTrue(inventory.Items.Any(i => i.Id == 3 && i.Name == "Bow"));
        }

        [TestMethod]
        public void GetItemById_ReturnsSeededItem()
        {
            ItemDTO item = itemRepo!.GetItemById(1);

            Assert.AreEqual(1, item.Id);
            Assert.AreEqual(1, item.GameId);
            Assert.AreEqual("Sword", item.Name);
            Assert.AreEqual("A sharp sword", item.Description);
        }

        [TestMethod]
        public void CreateItem_AddsNewItem()
        {
            GameDTO game = gameRepo!.GetAllGames().First();

            string itemName = "NewItemTest";
            string itemDesc = "Test description";

            bool created = itemRepo!.CreateItem(game.Id, itemName, itemDesc);
            Assert.IsTrue(created);

            List<ItemDTO> items = itemRepo.GetAllItemsByGameId(game.Id);
            Assert.IsTrue(items.Any(i => i.Name == itemName && i.Description == itemDesc));
        }

        [TestMethod]
        public void GetAllOffersByGameId_ReturnsOffers()
        {
            List<OfferDTO> offers = offerRepo!.GetAllOffersByGameId(1);

            Assert.IsTrue(offers.All(o => o.Item.GameId == 1));
        }

        [TestMethod]
        public void GetOffersBySearchAndGameId_FiltersByItemName()
        {
            List<OfferDTO> offers = offerRepo!.GetOffersBySearchAndGameId("Sword", 1);

            Assert.IsTrue(offers.All(o => o.Item.GameId == 1));
            Assert.IsTrue(offers.All(o => o.Item.Name.Contains("Sword")));
        }

        [TestMethod]
        public void GetAllTradesByGameId_ReturnsTrades()
        {
            List<TradeAdminDTO> trades = tradeRepo!.GetAllTradesByGameId(1);

            Assert.IsTrue(trades.Count > 0);
            Assert.IsTrue(trades.All(t => t.OffererName != null && t.TraderName != null));
        }

        [TestMethod]
        public void GetTradeByTradeId_ReturnsCorrectTrade()
        {
            TradeDTO trade = tradeRepo!.GetTradeByTradeId(1);

            Assert.IsTrue(trade.TradeOffers.Count > 0);
        }

        [TestMethod]
        public void GetUserById_ReturnsSeededUser()
        {
            UserDTO user = userRepo!.GetUserById(1);

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Alice", user.Username);
            Assert.AreEqual(UserDTO.UserRole.User, user.Role);
        }

        [TestMethod]
        public void GetUserIdByLogin_ReturnsCorrectId()
        {
            int aliceId = userRepo!.GetUserIdByLogin("Alice", "password123");
            Assert.AreEqual(1, aliceId);

            int invalid = userRepo.GetUserIdByLogin("NonExistent", "nopass");
            Assert.AreEqual(0, invalid);
        }
    }
}
