using LootTradeDomainModels;
using LootTradeDTOs;
using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeServices
{
    public class InventoryService
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public Inventory GetInventoryByUserIdAndGameId(int userId, int gameId)
        {
            Inventory inventory = new Inventory();

            InventoryDTO inventoryDTO = inventoryRepository.GetInventoryByUserIdAndGameId(userId, gameId);

            if (inventoryDTO.Items != null)
            {

                foreach (ItemDTO ItemDTO in inventoryDTO.Items)
                {
                    Item item = new Item();
                    item.Id = ItemDTO.Id;
                    item.GameId = ItemDTO.GameId;
                    item.Name = ItemDTO.Name;
                    item.Description = ItemDTO.Description;
                    inventory.Items.Add(item);
                }
            }

            return inventory;
        }
    }
}
