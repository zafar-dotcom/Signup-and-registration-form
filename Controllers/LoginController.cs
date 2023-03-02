using Complete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace Complete.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin_post([Bind] User_model_RBAC user)
        { 
           var users=new User_model_RBAC();
            var createclaim=users.GetUsers().Where(p=>p.UserName==user.UserName).SingleOrDefault();
           // var alluser = users.GetUsers().FirstOrDefault();
            if (users.GetUsers().Any(u => u.UserName == user.UserName))
            {
                var userclaims = new List<Claim>()
                {
                    new Claim("UserName",createclaim.UserName),
                    new Claim(ClaimTypes.Name,createclaim.Name),
                    new Claim(ClaimTypes.Email,createclaim.EmailId),
                    new Claim(ClaimTypes.DateOfBirth,createclaim.DateOfBirth),
                    new Claim(ClaimTypes.Role,createclaim.Role)

                };

                var grandmaidentity = new ClaimsIdentity(userclaims, "User Identity");
                var userprincipal = new ClaimsPrincipal(new[] { grandmaidentity });
                HttpContext.SignInAsync(userprincipal);
                return RedirectToAction("Get_all", "Home");


            }
            return RedirectToAction("UserLogin",user);
        }
        [HttpGet]
        public ActionResult UserAccessDenied()
        {
            return View();
        }
    }
}
