using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using _12591_API.Modals;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _12591_API.DTOs;
using _12591_API.Data;

namespace _12591_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            // Validate user credentials
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Create the JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // Helper method to generate JWT
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim("username", user.Username),
                new Claim("email", user.Email),
                new Claim("user_id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // POST: api/token/verify
        [HttpPost("verify")]
        public IActionResult VerifyToken([FromBody] TokenRequest tokenRequest)
        {
            if (string.IsNullOrEmpty(tokenRequest.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            try
            {
                // Validate and decode the token
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(tokenRequest.Token))
                {
                    return BadRequest(new { message = "Invalid token format" });
                }

                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true // Ensures token has not expired
                };

                var principal = tokenHandler.ValidateToken(tokenRequest.Token, validationParameters, out SecurityToken validatedToken);

                // Extract token claims
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return Unauthorized(new { message = "Invalid token format" });
                }

                var claims = jwtToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);

                return Ok(new
                {
                    message = "Token is valid",
                    claims
                });
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new { message = "Token has expired" });
            }
            catch (SecurityTokenException)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        public class TokenRequest
        {
            public string Token { get; set; }
        }
    }
}
