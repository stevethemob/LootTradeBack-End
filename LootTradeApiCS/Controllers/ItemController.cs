using Microsoft.AspNetCore.Mvc;
using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        readonly ItemService itemService;

        public ItemController(ItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet("ByItemId/{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetItemById(int itemId)
        {
            Item item = itemService.GetItemById(itemId);

            if (item == null)
            {
                return NotFound("Item with id" + itemId + " not found.");
            }

            return Ok(item);
        }

        [Authorize]
        [HttpGet("ByGame/{gameId}")]
        [ProducesResponseType(typeof(List<Item>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult GetAllItems(int gameId)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized("User Id not found in token.");
            }

            List<Item> items = itemService.GetAllItemsByGameId(gameId);

            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Item), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult createItem([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("Item data is required.");
            }

            bool succes = itemService.CreateItem(item.Name, item.Description);

            if (!succes)
            {
                return StatusCode(500, "Failed to create item.");
            }

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("editItem")]
        public IActionResult editItem([FromBody] Item item)
        {
            if (!itemService.EditItem(item))
            {
                return StatusCode(500, "Failed to edit item.");
            }

            return Ok();
        }
    }
}
