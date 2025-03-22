using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unione.Models;

namespace Unione.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly AppDBContext _dbContext;

        public UserController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserModel user )
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            // Optionally, add validation logic here (e.g., check if the email already exists)
            if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                return Conflict("Email already exists.");
            }

            // Add the user to the database
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Return the created user with HTTP 201 status code
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
