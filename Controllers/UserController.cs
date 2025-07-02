using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/authenticate?email=...&password=...
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] string email, [FromQuery] string password)
        {
            var user = await _userService.Authenticate(email, password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user);
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var createdUser = await _userService.Register(user);
            return Ok(createdUser);
        }
    }
}
