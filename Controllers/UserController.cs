using Microsoft.AspNetCore.Mvc;
using AI_Friendly_Calendar.Models;
using System.Linq;

namespace AI_Friendly_Calendar.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    // [ApiExplorerSettings(GroupName = "tools")]  // User actions = tools (executable actions)
    public class UserController : ControllerBase
    {
        private readonly CalendarDbContext _context;

        public UserController(CalendarDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("User with this email already exists.");

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok($"Welcome, {user.Name}!");
        }
    }
}
