using LootTradeDTOs;
using LootTradeInterfaces;
using MySql.Data.MySqlClient;

namespace LootTradeRepositories
{
    public class ItemRepository : IItemRepository
    {
        string connString = "";

        public ItemRepository(string connString)
        {
            this.connString = connString;
        }
        
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
