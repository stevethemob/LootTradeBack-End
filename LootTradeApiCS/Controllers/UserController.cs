using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Mvc;

namespace LootTradeApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {   
            User user = userService.GetUserById(id);

            if (user == null)
            {
                return NotFound("Item with id" + id + " not found.");
            }

            return Ok(user);
        }
    }
}
