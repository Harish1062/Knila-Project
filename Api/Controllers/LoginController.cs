
using KnilaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KnilaProject.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var result = new Result<string>();
            var user = Authenticate(userLogin);
            if (user != null)
            {
                result.Data = GenerateToken(user);
                return Ok(result);
            }

            return BadRequest("Invalid User");
        }

        // To generate token
        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKeyes:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["JWTKeyes:ValidIssuer"],
                _config["JWTKeyes:ValidAudience"],
                claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = StaticUserList.Users.FirstOrDefault(x => x.Username.ToLower() ==
                userLogin.email.ToLower() && x.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        [HttpPost("check-token-valid")]
        public IActionResult CheckTokenValid(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var AuthToken = tokenHandler.ReadJwtToken(token);
            if (AuthToken.ValidTo > DateTime.UtcNow)
                Ok(false);
            return Ok(true);
        }
    }
}
