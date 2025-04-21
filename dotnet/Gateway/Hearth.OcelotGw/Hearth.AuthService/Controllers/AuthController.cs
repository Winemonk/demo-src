using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hearth.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var key = Encoding.UTF8.GetBytes("12345678901234561234567890123456");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "test_user"),
                new Claim(ClaimTypes.Name, "test_user"),
                new Claim(ClaimTypes.NameIdentifier, "test_user_id"),
                new Claim(ClaimTypes.Role, "admin")
            };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(30);
            var token = new JwtSecurityToken(
                issuer: "Hearth.AuthService",
                audience: "Hearth.OcelotGw",
                claims: claims,
                expires: expires,
                signingCredentials: credentials);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { success = true, message = "Login successful!", token = tokenString, expiration = token.ValidTo });
        }
    }
}
