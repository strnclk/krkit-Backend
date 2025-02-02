using krkit_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using krkit_Backend.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("MySuperSecretKey12345678901234567890123456789012!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "krkit-backend",
                Audience = "krkit-backend"
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Kullanıcı adı ve şifre gereklidir.");

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User { Username = request.Username, PasswordHash = hashedPassword };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

            return Ok(new { Token = GenerateToken(user.Username) });
        }
    }
}
