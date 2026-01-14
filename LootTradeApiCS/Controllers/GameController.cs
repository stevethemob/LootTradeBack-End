using Microsoft.AspNetCore.Mvc;
using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesResponseType(typeof(List<Game>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllGames()
        {
            List<Game> games = gameService.GetAllGames();

            if (games.Count == 0)
            {
                return StatusCode(500, "Failed to get all games");
            }

            return Ok(games);
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 500)]
        [HttpPost("{gameTitle}")]
        public IActionResult AddGame(string gameTitle) 
        {

            if (!gameService.AddGame(gameTitle))
            {
                return StatusCode(500, "adding the game failed");
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [HttpPut("{gameId}/{gameTitle}")]
        public IActionResult EditGame(int gameId, string gameTitle)
        {
            if (!gameService.EditGameWithGameId(gameId, gameTitle))
            {
                return StatusCode(404, "gameId not found");
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Game), 200)]
        [HttpGet("{gameId}")]
        public IActionResult GetGameById(int gameId)
        {
            Game game = gameService.GetGameByGameId(gameId);

            if (game == null)
            {
                return StatusCode(404, "game not found with id: " + gameId);
            }

            return Ok(game);
        }
    }
}
