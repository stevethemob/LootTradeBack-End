using LootTradeDTOs;
using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockRepositories
{
    public class ItemRepositoryMock : IItemRepository
    {
        public ItemDTO GetItemById(int id)
        {
            ItemDTO item = new ItemDTO(id, 1 ,"Test", "Test");
            return item;
        }

        public List<ItemDTO> GetItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            ItemDTO item1 = new ItemDTO(1, 1, "Test", "Test");
            ItemDTO item2 = new ItemDTO(2, 2, "Test2", "Test2");
            
            items.Add(item1);
            items.Add(item2);

            return items;
        }

        public bool CreateItem(int gameId, string itemName, string itemDescription)
        {
            return true;
        }

        public List<ItemDTO> GetAllItemsByGameId(int gameId)
        {
            List<ItemDTO> items = new List<ItemDTO>();
            ItemDTO item1 = new ItemDTO(1, gameId, "Test", "Test");
            ItemDTO item2 = new ItemDTO(2, gameId, "Test2", "Test2");
            ItemDTO item3 = new ItemDTO(3, gameId, "Test3", "Test3");
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            return items;
        }

        public bool EditItem(ItemDTO item)
        {
            return true;
        }
    }
}
