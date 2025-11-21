using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Mvc;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        InventoryService inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetInventoryByUserId(int userId, int gameId)
        { 
            Inventory inventory = inventoryService.GetInventoryByUserIdAndGameId(userId, gameId);

            return Ok(inventory);
        }

        [HttpPost]
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
