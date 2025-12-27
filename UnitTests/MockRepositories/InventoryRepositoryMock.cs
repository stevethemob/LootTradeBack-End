using LootTradeDTOs;
using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockRepositories
{
    public class InventoryRepositoryMock : IInventoryRepository
    {
        public InventoryDTO GetInventoryByUserIdAndGameId(int userId, int gameId)
        {
            InventoryDTO inventory = new InventoryDTO();
            ItemDTO item1 = new ItemDTO(1, 1, "hello", "greeting");
            inventory.Items.Add(item1);
            return inventory;
        }

        public bool AddItemToUserTheirInventoryByUserIdAndItemId(int userId, int ItemId)
        {
            return true;
        }

        public int GetInventoryIdByUserIdAndItemId(int userId, int itemId)
        {
            return 1;
        }
    }
}
