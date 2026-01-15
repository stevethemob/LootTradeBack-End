using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GetAllGamesTest1Game()
        {
            GameRepositoryMock gameRepositoryMock = new GameRepositoryMock();
            GameService gameService = new GameService(gameRepositoryMock);
            List<Game> games = new List<Game>();

            games = gameService.GetAllGames();

            Assert.IsTrue(games.Count == 1);
        }

        [TestMethod]
        public void AddGameTest1True()
        {
            GameRepositoryMock gameRepositoryMock = new GameRepositoryMock();
            GameService gameService = new GameService(gameRepositoryMock);

            string gameTitle = "test1";

            bool success = gameService.AddGame(gameTitle);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void EditGameWithIdTest1()
        {
            GameRepositoryMock gameRepositoryMock = new GameRepositoryMock();
            GameService gameService = new GameService(gameRepositoryMock);

            int gameId = 1;
            string gameTitle = "test1";

            bool success = gameService.EditGameWithGameId(gameId, gameTitle);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetGameByGameIdTest1()
        {
            GameRepositoryMock gameRepositoryMock = new GameRepositoryMock();
            GameService gameService = new GameService(gameRepositoryMock);

            int gameId = 1;

            Game game = gameService.GetGameByGameId(gameId);

            Assert.IsNotNull(game.Title);
        }
    }
}
