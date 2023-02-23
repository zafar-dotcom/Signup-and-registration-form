using Complete.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser_sign_up ui;
        // private readonly SignInManager<User_signup_model> _signInManager;
        //  private readonly IHttpContextAccessor _httpContextAccessor;



        public UserController()
        {
            ui = new User_implement_interface("nothing");

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            bool result = ui.Verify(email, password);
            if (result == true)
            {
                TempData["LoginSuucessfull"] = "<script>alert ('Login successfull')</script>";
                return RedirectToAction("Get_all", "Home");
            }
            else
            {
                TempData["Loginfail"] = "<script>alert('Login failed') </script>";
                return View();
            }

        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User_signup_model usr)
        {
            ValidationError ve = new ValidationError();
            ve = ui.Validation(usr);
            if (ve.retval == true)
            {
                bool x = ui.Register(usr);
                if (x == true)

                {
                    TempData["Registrationsuccessfull"] = "You registration has been completed ,now sign in to access your data ";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["insertionerror"] = "Data insertion error";
                    return View();
                }
            }

            else
            {
                ViewBag.message = ve.msg;
                return View();
            }


        }
        //    [HttpGet]
        //    public IActionResult Login_wiht_google()
        //    {
        //        var returnUrl = _httpContextAccessor.HttpContext.Request.Query["returnUrl"];
        //        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, GoogleDefaults.AuthenticationScheme);
        //    }
        //    [HttpGet]
        //    public async Task<IActionResult> Logout()
        //    {
        //        await _signInManager.SignOutAsync();
        //        return RedirectToAction("Login", "User");

        //      public async Task Login()
        //      {
        //         await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        //         {
        //           RedirectUri = Url.Action("GoogleResponse")
        //       });
        //      }
        //     public ValidationError Validation(User_signup_model usr)
        //     {
        //       ValidationError obj = new ValidationError();

        //}
    }
}
