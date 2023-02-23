using Complete.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : Controller
    {
       
        private IConfiguration _config;
        public JwtController(IConfiguration config)
        {
                    _config= config;    
        }
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel_jwt usermodel)
        {
            IActionResult responce = Unauthorized();
           // var user = AuthenticateUser(usermodel);
            if (usermodel != null) {
                var tokenstring = GenerateJSONWEBToken(usermodel);
                responce=Ok(new {token=tokenstring});
            }
            return responce;
        }

        private string GenerateJSONWEBToken(UserModel_jwt userinfo)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credientiel = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Username),
        new Claim(JwtRegisteredClaimNames.Email, userinfo.EmailAddress),
        new Claim("Dateofjoining", userinfo.Dateofjoining.ToString("yyyy-MM-dd")),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token=new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:issuer"],claims,expires:DateTime.Now.AddMinutes(120) ,
                signingCredentials:credientiel
                
                );
            return new  JwtSecurityTokenHandler().WriteToken(token);    
        }
        /* private  static UserModel_jwt AuthenticateUser(UserModel_jwt login)
         {
             UserModel_jwt user = null;

             //Validate the User Credentials    
             //Demo Purpose, I have Passed HardCoded User Information    
             if (login.Username == "zafar")
             {
                 user = new UserModel_jwt { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
             }
             return user;
         }*/
        [Route("getdata")]
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[]
            {
                "Mohammad zafar",
                 "Mohammad zafar",
                  "Mohammad zafar",
                   "Mohammad zafar",

            };
        }
        [Route("getuser")]
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Getuser()
        {
            var currentuser = HttpContext.User;
            int spendtimewithcompany = 0;
            if(currentuser.HasClaim(c=>c.Type== "Dateofjoining") ) {
                DateTime date = DateTime.Parse(currentuser.Claims.FirstOrDefault(c => c.Type == "Dateofjoining").Value);
                spendtimewithcompany = DateTime.Today.Year - date.Year;
            }
            if (spendtimewithcompany > 5)
            {
                return new string[]
                {
                    "you are our senior employee",
                    "you are our senior employee",
                    "you are our senior employee",
                    "you are our senior employee",

                };
               
            }
            else
            {
                return new string[]
                {
                    "You are our new Employee",
                     "You are our new Employee",
                      "You are our new Employee",
                       "You are our new Employee",

                };
            }
        }
    }
}
