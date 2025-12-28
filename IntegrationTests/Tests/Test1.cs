using LootTradeRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LootTrade.IntegrationTests.TestFixtures;

namespace IntegrationTests.Tests
{
    [TestClass]
    public class GameRepositoryTests
    {
        private static MySqlDatabaseFixture? fixture;
        private static GameRepository? repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            fixture = new MySqlDatabaseFixture();
            repo = new GameRepository(fixture.ConnectionString);
        }

        [ClassCleanup(ClassCleanupBehavior.EndOfClass)]
        public static void Cleanup()
        {
            //fixture?.Dispose(); // prevents NullReferenceException
        }

        [TestMethod]
        public void GetAllGames_ReturnsSeededGames()
        {
            var games = repo!.GetAllGames();

            Assert.AreEqual(2, games.Count);
            Assert.IsTrue(games.Exists(g => g.Title == "Game A"));
            Assert.IsTrue(games.Exists(g => g.Title == "Game B"));
        }
    }
}
