using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Mvc;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        readonly InventoryService inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        [HttpGet("{userId}/{gameId}")]
        public IActionResult GetInventoryByUserId(int userId, int gameId)
        { 
            Inventory inventory = inventoryService.GetInventoryByUserIdAndGameId(userId, gameId);

            if (inventory == null)
            {
                return NotFound("couldn't find invenotry");
            }

            return Ok(inventory.Items);
        }

        [HttpPost("{userId}/{itemId}")]
        public IActionResult AddItemToUserTheirInventoryByUserIdAndItemId(int userId, int itemId)
        {
            if (itemId == 0 || userId == 0)
            {
                return BadRequest("itemId or userId was 0.");
            }

            bool succes = inventoryService.AddItemToUserTheirInventoryByUserIdAndItemId(userId, itemId);

            if (!succes)
            {
                return StatusCode(500, "Failed to add item to inventory.");
            }

            return Ok("Item was added to inventory.");
        }
    }
}
