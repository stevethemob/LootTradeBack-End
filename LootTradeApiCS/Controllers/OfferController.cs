using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        readonly OfferService offerService;

        public OfferController(OfferService offerService)
        {
            this.offerService = offerService;
        }

        [Authorize]
        [HttpPost("ByItemId")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult CreateOffer([FromBody] int itemId)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized("userId claim missing");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool success = offerService.AddOffer(userId, itemId);

            if (!success)
            {
                return NotFound("Item With id: " + itemId + " was not found");
            }

            return Ok();
        }

        [HttpGet("ByGameId/{gameId}")]
        [ProducesResponseType(typeof(List<AllOffers>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetAllOffers(int gameId)
        {
            List<Offer> offers = offerService.GetAllOffersByGameId(gameId);

            List<AllOffers> allOffers = new List<AllOffers>();

            foreach (Offer offer in offers)
            {
                AllOffers offerForTransfer = new AllOffers
                (
                offer.Id,
                offer.DateTimeOpen,
                offer.Item.Id,
                offer.Item.Name,
                offer.Item.Description
                );
                allOffers.Add(offerForTransfer);
            }

            if (allOffers.Count == 0)
            {
                return BadRequest("gameId was not found");
            }

            return Ok(allOffers);
        }

        [HttpGet("Search/{gameId}/{searchQuery}")]
        [ProducesResponseType(typeof(List<AllOffers>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult SearchOffer(string searchQuery, int gameId)
        {
            List<Offer> offers = offerService.GetOffersBySearchAndGameId(searchQuery, gameId);

            List<AllOffers> allOffers = new List<AllOffers>();

            foreach (Offer offer in offers)
            {
                AllOffers offerForTransfer = new AllOffers
                (
                offer.Id,
                offer.DateTimeOpen,
                offer.Item.Id,
                offer.Item.Name,
                offer.Item.Description
                );
                allOffers.Add(offerForTransfer);
            }

            if (allOffers.Count == 0)
            {
                return NotFound("couldn't find any offers");
            }

            return Ok(allOffers);
        }
    }
}

