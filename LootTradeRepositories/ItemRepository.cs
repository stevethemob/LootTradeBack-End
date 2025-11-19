using LootTradeDTOs;
using LootTradeInterfaces;

namespace LootTradeRepositories
{
    public class ItemRepository : IItemRepository
    {
        public ItemDTO GetItemById(int id)
        {
            ItemDTO item = new ItemDTO();
            item.Id = id;
            item.Name = "hallo";
            item.Description = "groeting";
            return item;
        }
        public bool CreateItem(ItemDTO itemDTO)
        {
            return true;
        }
    }
}
