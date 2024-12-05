using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _12591_API.Data;
using _12591_API.Modals;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace _12591_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/activity
        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            // Extract UserId from JWT token
            var userId = GetUserIdFromToken();

            var activities = await _context.Activities
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return Ok(activities);
        }

        // GET: api/activity/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var userId = GetUserIdFromToken();

            var activity = await _context.Activities
                .Where(a => a.Id == id && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // POST: api/activity
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
        {
            if (activity == null)
            {
                return BadRequest("Activity is null");
            }

            var userId = GetUserIdFromToken();

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            activity.UserId = user.Id;

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
        }

        // PUT: api/activity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest("Activity ID mismatch");
            }

            var userId = GetUserIdFromToken();

            var existingActivity = await _context.Activities
                .Where(a => a.Id == id && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingActivity == null)
            {
                return NotFound();
            }

            existingActivity.ActivityType = activity.ActivityType;
            existingActivity.Duration = activity.Duration;
            existingActivity.CaloriesBurned = activity.CaloriesBurned;
            existingActivity.Date = activity.Date;

            _context.Entry(existingActivity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/activity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var userId = GetUserIdFromToken();

            var activity = await _context.Activities
                .Where(a => a.Id == id && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirstValue("user_id");
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException("User ID not found in the token");
            }
            return int.Parse(userIdClaim);
        }
    }
}
