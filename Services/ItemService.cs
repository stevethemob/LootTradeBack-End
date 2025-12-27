using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeDomainModels;

namespace LootTradeServices;

public class ItemService
{
    private readonly IItemRepository itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public Item GetItemById(int id)
    {
        ItemDTO itemDTO = itemRepository.GetItemById(id);
        Item item = new Item
        (
        itemDTO.Id,
        itemDTO.GameId,
        itemDTO.Name,
        itemDTO.Description
        );
        return item;
    }

    public bool CreateItem(string itemName, string itemDescription)
    {   
        return itemRepository.CreateItem(itemName, itemDescription);
    }

    public List<Item> GetAllItemsByGameId(int gameId)
    {
        List<Item> items = new List<Item>();

        List<ItemDTO> itemDTOs = itemRepository.GetAllItemsByGameId(gameId);

        foreach (ItemDTO itemDTO in itemDTOs)
        {
            Item item = new Item
            (
            itemDTO.Id,
            itemDTO.GameId,
            itemDTO.Name,
            itemDTO.Description
            );
            items.Add(item);
        }

        return items;
    }
}
