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
        readonly UserService userService;

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
        public IActionResult createUser([FromBody] RegisterRequest dto)
        {
            ValidatorResponse validation = userService.CreateUser(dto.Username, dto.Password, dto.Email, dto.RepeatedPassword);

            if(!validation.Success)
            {
                return BadRequest(validation);
            }

            return Ok(validation);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest dto, [FromServices] JwtService jwt)
        {
            int userId = userService.GetUserIdByLogin(dto.Username, dto.Password);

            if (userId == 0)
            {
                return Unauthorized("Invalid username or password");
            }

            string token = jwt.GenerateToken(userId, dto.Username);

            return Ok(new {token});
        }
    }
}
