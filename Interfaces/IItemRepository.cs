using LootTradeDTOs;

namespace LootTradeInterfaces
{
    public interface IItemRepository
    {
        public ItemDTO GetItemById(int itemId);
        public bool CreateItem(ItemDTO itemDTO);

        public List<ItemDTO> GetAllItemsByGameId(int gameId);
    }
}
