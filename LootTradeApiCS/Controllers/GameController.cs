using Microsoft.AspNetCore.Mvc;
using LootTradeDomainModels;
using LootTradeServices;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        GameService gameService;

        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            List<Game> games = new List<Game>();

            games = gameService.GetAllGames();

            return Ok(games);
        }
    }
}
