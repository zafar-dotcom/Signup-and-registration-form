using Complete.DAL_Services;
using Complete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Complete.Controllers
{
  // [Produces()]
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IUser_Login_Registraion _interface;
        private IConfiguration _config;

        public JWTController(IConfiguration config, IUser_Login_Registraion inter)
        {
            _config = config;
            _interface = inter;

        }
        [Route("Login")]
        [AllowAnonymous]
        [HttpPost]
        
        public IActionResult Login([FromBody] User_model_jwt userLogin)
        {
            bool user =_interface.Authenticate(userLogin);

            if (user == true)
            {
                var token = Generate(userLogin);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(User_model_jwt user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}
