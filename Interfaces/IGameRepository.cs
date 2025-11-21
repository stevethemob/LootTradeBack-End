using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootTradeDTOs;

namespace LootTradeInterfaces
{
    public interface IGameRepository
    {
        public List<GameDTO> GetAllGames();
    }
}
