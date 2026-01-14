using LootTradeDTOs;

namespace LootTradeInterfaces
{
    public interface IItemRepository
    {
        public ItemDTO GetItemById(int itemId);
        public bool CreateItem(string itemName, string itemDescription);

        public List<ItemDTO> GetAllItemsByGameId(int gameId);
        public bool EditItem(ItemDTO item);
    }
}
