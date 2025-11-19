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

        [HttpGet("{Id}")]
        public IActionResult GetItemById(int Id)
        {
            Item item = itemService.GetItemById(Id);

            if (item == null)
            {
                return NotFound("Item with id" + Id + " not found.");
            }

            return Ok(item);
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
