using Complete.DAL_Services;
using Complete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    [Route("api/[controller]")]
  //  [ApiController]
    public class RegisterController : Controller
    {
        private readonly IUser_Login_Registraion _urepp;
        public RegisterController(IUser_Login_Registraion urepp)
        {
            _urepp = urepp;
        }
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login_user(string email, string password)
        {
            bool result = _urepp.Verfify(email,password);
            if (result == true)
            {
                TempData["login_success"] = "<script>alert('Login Successfull')</script>";
                return RedirectToAction("Get_all", "Home");
            }
            else
            {
              //  TempData["login_fail"] = "<script>alert('Login failed')</script>";

                ViewBag.message = "Login Failed";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Registar_user(User_model_login user)
        {
            ValidationError ve = new ValidationError();
          ve= _urepp.Validate(user);
            if (ve.retval == true)
            {
              bool x=_urepp.Registration(user);
                if (x==true)
                {
                    TempData["registered"] = "<script>alert('Registered Successfully')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["not_registered"] = "insertion failed";
                    return View("Registration", user);
                }
            }
            else
            {

                ViewBag.message = ve.retmsg;
                return View("Registration",user);
                
            }

           


        }


    }
}
