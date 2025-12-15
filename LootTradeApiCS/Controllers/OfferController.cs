using LootTradeServices;
using LootTradeDomainModels;
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

        [HttpGet("ByGameId/{gameId}")]
        public IActionResult GetAllOffers(int gameId)
        {
            List<Offer> offers = offerService.GetAllOffersByGameId(gameId);

            List<AllOffers> allOffers = new List<AllOffers>();

            foreach (Offer offer in offers)
            {
                AllOffers offerForTransfer = new AllOffers();

                offerForTransfer.Id = offer.Id;
                offerForTransfer.DateTimeOpen = offer.DateTimeOpen;
                offerForTransfer.ItemId = offer.Item.Id;
                offerForTransfer.itemName = offer.Item.Name;
                offerForTransfer.itemDescription = offer.Item.Description;
                allOffers.Add(offerForTransfer);
            }

            if (allOffers == null || allOffers.Count == 0)
            {
                return BadRequest("gameId was not found");
            }

            return Ok(allOffers);
        }
    }
}

