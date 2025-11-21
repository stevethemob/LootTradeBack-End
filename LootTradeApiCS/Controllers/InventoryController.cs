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
    }
}
