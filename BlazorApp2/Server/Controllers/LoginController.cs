using BlazorApp2.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BlazorApp2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration) => _configuration = configuration;

        [HttpPost]
        public LoginResult Login(Credentials credentials)
        {
            var expiry = DateTime.Now.AddMinutes(2);
            return ValidateCredentials(credentials) ? new LoginResult { Token = GenerateJWT(credentials.Email, expiry), Expiry = expiry } : new LoginResult();
        }

        bool ValidateCredentials(Credentials credentials)
        {
            var user = _configuration.GetSection("Credentials").Get<Credentials>();
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.VerifyHashedPassword(null, user.Password, credentials.Password) == PasswordVerificationResult.Success;
        }

        private string GenerateJWT(string email, DateTime expiry)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                new[] { new Claim(ClaimTypes.Name, email) },
                expires: expiry,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
