using LootTradeServices;
using Microsoft.AspNetCore.Mvc;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        OfferService offerService;

        public OfferController(OfferService offerService)
        {
            this.offerService = offerService;
        }

        [HttpPost("ByInventoryId/{inventoryId}")]
        public IActionResult CreateOffer(int inventoryId)
        {
            bool success = offerService.AddOffer(inventoryId);

            if (!success)
            {
                return NotFound("Inventory With id: " + inventoryId + " was not found");
            }

            return Ok();
        }
    }
}

