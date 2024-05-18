using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTokenUsingMVCCore.Controllers
{
    public class SecurityController : Controller
    {     
        private string GenerateJsonWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my SecretKey for creating JwtToken for authentication")); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Issuer", "Test.com"),
                new Claim("Admin", "true"),
                new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            var token = new JwtSecurityToken("Test.com",
                "Test.com",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: SecurityController
        [HttpGet]
        public string Get()
        {
            return GenerateJsonWebToken("Shush");
        }
    }
}
