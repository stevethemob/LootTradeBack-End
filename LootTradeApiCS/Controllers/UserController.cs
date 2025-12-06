using LootTradeDomainModels;
using LootTradeServices;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;

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

        [HttpPost("register")]
        public IActionResult createUser([FromBody] User dto)
        {
            User user = new User();
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.RepeatedPassword = dto.RepeatedPassword;
            user.Email = dto.Email;

            ValidatorResponse validation = userService.CreateUser(user, user.RepeatedPassword);

            if(!validation.Success)
            {
                return BadRequest(validation);
            }

            return Ok(validation);
        }

        [HttpGet("login/{username}/{password}")]
        public IActionResult login(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;

            int userId = userService.GetUserIdByLogin(user);

            return Ok(userId);
        }
    }
}
