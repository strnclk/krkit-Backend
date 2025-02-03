using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using krkit_Backend.Data.Models;
using krkit_Backend.Data.Models.DTOs;
using krkit_Backend.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration; // IConfiguration'i ekliyoruz

    public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration; // IConfiguration'ı enjekte ediyoruz

    }


    private string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Kullanıcı adı ve şifre gereklidir.");

        if (await _unitOfWork.Users.IsExistByUserNameAsync(request.Username))
            return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User { Username = request.Username, PasswordHash = hashedPassword };

        await _unitOfWork.Users.AddAsync(user);

        return Ok("Kullanıcı başarıyla oluşturuldu.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByNameAsync(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

            return Ok(new { Token = GenerateToken(user.Username) });
        }
        catch (Exception e)
        {

            return Ok("ERROR");
        }
  
    }
}
