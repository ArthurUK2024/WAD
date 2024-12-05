using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _12591_API.Data;
using _12591_API.Modals;
using Microsoft.EntityFrameworkCore;

namespace _12591_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (newUser == null || string.IsNullOrWhiteSpace(newUser.Username) || string.IsNullOrWhiteSpace(newUser.Email))
            {
                return BadRequest("Invalid user data.");
            }

            newUser.Activities = null;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
