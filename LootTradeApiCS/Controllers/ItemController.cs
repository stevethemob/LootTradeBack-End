using Microsoft.AspNetCore.Mvc;
using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Components.Web;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        ItemService itemService;

        public ItemController(ItemService itemService)
        {   
            this.itemService = itemService;
        }

        [HttpGet("ByItemId{itemId}")]
        public IActionResult GetItemById(int itemId)
        {
            Item item = itemService.GetItemById(itemId);

            if (item == null)
            {
                return NotFound("Item with id" + itemId + " not found.");
            }

            return Ok(item);
        }

        [HttpGet("ByGame/{gameId}")]
        public IActionResult GetAllItems(int gameId)
        {
            List<Item> items = new List<Item>();

            items = itemService.GetAllItemsByGameId(gameId);

            return Ok(items);
        }

        [HttpPost]
        public IActionResult createItem([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("Item data is required.");
            }

            bool succes = itemService.CreateItem(item);

            if (!succes)
            {
                return StatusCode(500, "Failed to create item.");
            }

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }
    }
}
