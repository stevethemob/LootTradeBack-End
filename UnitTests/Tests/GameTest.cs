using LootTradeDomainModels;
using LootTradeServices;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public sealed class GameTest
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
    }
}
