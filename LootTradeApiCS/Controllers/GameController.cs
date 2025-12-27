using Microsoft.AspNetCore.Mvc;
using LootTradeDomainModels;
using LootTradeServices;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        readonly GameService gameService;

        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            List<Game> games = gameService.GetAllGames();

            if (games.Count == 0)
            {
                return StatusCode(500, "Failed to get all games");
            }

            return Ok(games);
        }
    }
}
