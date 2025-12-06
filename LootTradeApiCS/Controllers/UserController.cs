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

        [HttpPost("{username}/{password}/{repeatedPassword}/{email}")]
        public IActionResult createUser(string username, string password, string repeatedPassword, string email)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Email = email;

            ValidatorResponse validation = userService.CreateUser(user, repeatedPassword);

            if(!validation.Success)
            {
                return BadRequest(validation);
            }

            return Ok(validation);
        }
    }
}
