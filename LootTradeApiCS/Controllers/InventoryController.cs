using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("{userId}/{gameId}")]
        [ProducesResponseType(typeof(List<Item>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetInventoryByUserId(int gameId)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            int userId = int.Parse(userIdClaim.Value);

            Inventory inventory = inventoryService.GetInventoryByUserIdAndGameId(userId, gameId);

            if (inventory == null)
            {
                return NotFound("couldn't find inventory");
            }

            return Ok(inventory.Items);
        }

        [HttpPost("{userId}/{itemId}")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
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
