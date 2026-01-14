using LootTradeDTOs;
using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockRepositories
{
    public class GameRepositoryMock : IGameRepository
    {
        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> gameDTOs = new List<GameDTO>();

            GameDTO gameDTO = new GameDTO(1,"test");
            gameDTOs.Add(gameDTO);

            return gameDTOs;
        }

        public bool AddGame(string gameTitle)
        {
            return true;
        }

        public bool GameExistsWithTitle(string gameTitle)
        {
            return false;
        }

        public bool EditGameWithGameId(int gameId, string gameTitle)
        {
            return true;
        }
    }
}
