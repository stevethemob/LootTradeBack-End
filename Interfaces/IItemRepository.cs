using LootTradeDTOs;

namespace LootTradeInterfaces
{
    public interface IItemRepository
    {
        public ItemDTO GetItemById(int Id);
        public bool CreateItem(ItemDTO itemDTO);

        public List<ItemDTO> GetAllItemsByGameId(int gameId);
    }
}
