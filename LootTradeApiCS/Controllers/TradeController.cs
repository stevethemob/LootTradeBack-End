using LootTradeDomainModels;
using LootTradeRepositories;
using LootTradeServices;
using Microsoft.AspNetCore.Mvc;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        readonly TradeService tradeService;

        public TradeController(TradeService tradeService)
        {
            this.tradeService = tradeService;
        }

        [HttpPost]
        public IActionResult AddTradeOffer([FromBody] TradeRequest request)
        {
            bool succes = tradeService.AddTradeOffer(request.OfferId, request.ItemIds, request.TraderId);

            if (!succes)
            {
                return StatusCode(500, "Adding the offer failed.");
            }

            return Ok();
        }
    }
}
