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
        Item item = new Item();
        item.Id = itemDTO.Id;
        item.Name = itemDTO.Name;
        item.Description = itemDTO.Description;
        return item;
    }

    public bool CreateItem(Item item)
    {   
        ItemDTO itemDTO = new ItemDTO();
        itemDTO.Name = item.Name;
        itemDTO.Description = item.Description;

        return itemRepository.CreateItem(itemDTO);
    }
}
