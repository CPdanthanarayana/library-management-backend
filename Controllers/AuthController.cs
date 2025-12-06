using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(User userData)
        {
            // check username exists
            if (await _db.Users.AnyAsync(u => u.Username == userData.Username))
                return BadRequest("Username already exists");

            // hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.PasswordHash);

            var user = new User
            {
                Username = userData.Username,
                PasswordHash = hashedPassword
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(User loginData)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == loginData.Username);

            if (user == null)
                return BadRequest("User not found");

            // check password
            if (!BCrypt.Net.BCrypt.Verify(loginData.PasswordHash, user.PasswordHash))
                return BadRequest("Invalid password");

            // generate JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"]!
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Issuer"],
                Audience = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

    }
}
