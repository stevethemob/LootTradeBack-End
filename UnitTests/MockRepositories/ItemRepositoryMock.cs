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
            ItemDTO item = new ItemDTO();
            item.Id = id;
            item.GameId = 1;
            item.Name = "Test";
            item.Description = "Test";
            return item;
        }

        public List<ItemDTO> GetItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            ItemDTO item1 = new ItemDTO();
            item1.Id = 1;
            item1.GameId = 1;
            item1.Name = "Test";
            item1.Description = "Test";
            ItemDTO item2 = new ItemDTO();
            item2.Id = 2;
            item2.GameId = 2;
            item2.Name = "Test2";
            item2.Description = "Test2";
            
            items.Add(item1);
            items.Add(item2);

            return items;
        }

        public bool CreateItem(ItemDTO item)
        {
            return true;
        }

        public List<ItemDTO> GetAllItemsByGameId(int gameId)
        {
            List<ItemDTO> items = new List<ItemDTO>();
            ItemDTO item1 = new ItemDTO();
            item1.Id = 1;
            item1.GameId = gameId;
            item1.Name = "Test";
            item1.Description = "Test";
            ItemDTO item2 = new ItemDTO();
            item2.Id = 2;
            item2.GameId = gameId;
            item2.Name = "Test2";
            item2.Description = "Test2";
            ItemDTO item3 = new ItemDTO();
            item3.Id = 3;
            item3.GameId = gameId;
            item3.Name = "Test3";
            item3.Description = "Test3";
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            return items;
        }
    }
}
