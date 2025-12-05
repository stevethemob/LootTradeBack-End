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

            GameDTO gameDTO = new GameDTO();
            gameDTO.Id = 1;
            gameDTO.Title = "test";
            gameDTOs.Add(gameDTO);

            return gameDTOs;
        }
    }
}
