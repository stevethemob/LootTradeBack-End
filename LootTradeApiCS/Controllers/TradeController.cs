using LootTradeDomainModels;
using LootTradeRepositories;
using LootTradeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost]
        public IActionResult AddTradeOffer([FromBody] TradeRequest request)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            int traderId = int.Parse(userIdClaim.Value);

            bool succes = tradeService.AddTradeOffer(request.OfferId, request.ItemIds, traderId);

            if (!succes)
            {
                return StatusCode(500, "Adding the offer failed.");
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllTradesByUserIdAndGameId(int userId, int gameId)
        {
            AllTrades allTrades = tradeService.GetAllTradeIdsByGameIdAndUserId(gameId, userId);

            return Ok(allTrades);
        }

        [HttpGet("ByTradeId/{tradeId}")]
        public IActionResult GetTradeById(int tradeId)
        {
            Trade trade = tradeService.GetTradeByTradeId(tradeId);

            return Ok(trade);
        }
    }
}
