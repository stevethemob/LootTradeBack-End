using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeDomainModels;

namespace LootTradeServices
{
    public class GameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();
            List<GameDTO> gameDTOs = gameRepository.GetAllGames();
            foreach (GameDTO gameDTO in gameDTOs)
            {
                Game game = new Game
                (
                gameDTO.Id,
                gameDTO.Title
                );
                games.Add(game);
            }

            return games;
        }

        public bool AddGame(string gameTitle)
        {
            if (gameRepository.GameExistsWithTitle(gameTitle))
            {
                return false;
            }

            return gameRepository.AddGame(gameTitle);
        }

        public bool EditGameWithGameId(int gameId, string gameTitle)
        {
            return gameRepository.EditGameWithGameId(gameId, gameTitle);
        }
    }
}
