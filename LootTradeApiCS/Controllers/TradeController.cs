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
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(void), 200)]
        [HttpPost("AddTradeOffer")]
        public IActionResult AddTradeOffer([FromBody] TradeRequest request)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized("User Id not found in token.");
            }

            int traderId = int.Parse(userIdClaim.Value);

            bool succes = tradeService.AddTradeOffer(request.OfferId, request.ItemIds, traderId);

            if (!succes)
            {
                return StatusCode(500, "Adding the offer failed.");
            }

            return Ok();
        }

        [Authorize]
        [ProducesResponseType(typeof(AllTrades), 200)]
        [HttpGet("GetAllTradesByGameId/{gameId}")]
        public IActionResult GetAllTradesByUserIdAndGameId(int gameId)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized("User Id not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            AllTrades allTrades = tradeService.GetAllTradeIdsByGameIdAndUserId(gameId, userId);

            return Ok(allTrades);
        }

        [ProducesResponseType(typeof(Trade), 200)]
        [HttpGet("ByTradeId/{tradeId}")]
        public IActionResult GetTradeById(int tradeId)
        {
            Trade trade = tradeService.GetTradeByTradeId(tradeId);

            return Ok(trade);
        }

        [Authorize]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(void), 200)]
        [HttpPost("AcceptTradeByTradeId/{tradeId}")]
        public IActionResult AcceptTrade(int tradeId)
        {
            Claim? userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized("User Id not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool success = tradeService.AcceptTrade(tradeId, userId);

            if (!success)
            {
                return StatusCode(500, "accepting the trade failed"); 
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<TradeAdmin>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [HttpGet("GetAllTradesByGameIdAdmin/{gameId}")]
        public IActionResult GetAllTradesByGameId(int gameId)
        {
            List<TradeAdmin> trades = tradeService.GetAllTradesByGameId(gameId);

            if (trades.Count == 0)
            {
                return StatusCode(404, "No trades were found");
            }

            return Ok(trades);
        }
    }
}
