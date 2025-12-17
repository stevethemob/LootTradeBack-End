using LootTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeInterfaces
{
    public interface IInventoryRepository
    {
        public InventoryDTO GetInventoryByUserIdAndGameId(int userId, int gameId);

        public bool AddItemToUserTheirInventoryByUserIdAndItemId(int userId, int itemId);

        public int GetInventoryIdByUserIdAndItemId(int userId, int itemId);
    }
}
