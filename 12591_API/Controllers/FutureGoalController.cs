using _12591_API.Data;
using _12591_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace _12591_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FutureGoalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FutureGoalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/futuregoal
        [HttpGet]
        public async Task<IActionResult> GetFutureGoals()
        {
            // Extract UserId from JWT token
            var userId = GetUserIdFromToken();

            var futureGoals = await _context.FutureGoals
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return Ok(futureGoals);
        }

        // GET: api/futuregoal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFutureGoal(int id)
        {
            var userId = GetUserIdFromToken();

            var goal = await _context.FutureGoals
                .Where(f => f.Id == id && f.UserId == userId)
                .FirstOrDefaultAsync();

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // POST: api/futuregoal
        [HttpPost]
        public async Task<IActionResult> CreateFutureGoal([FromBody] FutureGoal futureGoal)
        {
            if (futureGoal == null)
            {
                return BadRequest("FutureGoal is null");
            }

            var userId = GetUserIdFromToken();

            futureGoal.UserId = userId;

            _context.FutureGoals.Add(futureGoal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFutureGoal), new { id = futureGoal.Id }, futureGoal);
        }

        // PUT: api/futuregoal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFutureGoal(int id, [FromBody] FutureGoal futureGoal)
        {
            if (id != futureGoal.Id)
            {
                return BadRequest("FutureGoal ID mismatch");
            }

            var userId = GetUserIdFromToken();

            var existingGoal = await _context.FutureGoals
                .Where(f => f.Id == id && f.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingGoal == null)
            {
                return NotFound();
            }

            existingGoal.GoalName = futureGoal.GoalName;
            existingGoal.Description = futureGoal.Description;
            existingGoal.TargetDate = futureGoal.TargetDate;

            _context.Entry(existingGoal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/futuregoal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFutureGoal(int id)
        {
            var userId = GetUserIdFromToken();

            var goal = await _context.FutureGoals
                .Where(f => f.Id == id && f.UserId == userId)
                .FirstOrDefaultAsync();

            if (goal == null)
            {
                return NotFound();
            }

            _context.FutureGoals.Remove(goal);
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
