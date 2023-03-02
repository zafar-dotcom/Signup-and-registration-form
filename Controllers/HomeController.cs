using Complete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;

namespace Complete.Controllers
{
   // [Authorize(Policy = "UserPolicy")]
   // [Authorize(Roles ="Admin")]

    public class HomeController : Controller
    {
        DAL dal = new DAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();  
        }
        [Authorize]
        //[Authorize(Roles ="admin,seller")]
        public IActionResult Get_all()
        {
            return View(dal.GetEmployee());
        }
        [Authorize(Policy = "UserPolicy")]
        public IActionResult UserPolicy()
        {
            return RedirectToAction("Get_all");
        }

        [Authorize(Roles = "User")]
        public IActionResult UsersRole()
        {
            return RedirectToAction("Get_all");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminUser()
        {
            return RedirectToAction("Get_all");

        }
        public IActionResult Create()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserModel mdl)
        {
            if(dal.Add_user(mdl))
            {
                TempData["Insertmessage"] = "<script>alert('Inseerted successfully')</script>";
                return RedirectToAction("Get_all");

            }
            else
            {
                TempData["inserterror"] = "<script>alert('not inserted')</script>";
              
            }
            return View();

        }
        public IActionResult Update(int id)
        {
            return View(dal.GetEmployee().Find(x=>x.Id== id));
        }
        [HttpPost]
        public IActionResult Update(UserModel mdl)
        {
            if (dal.Update(mdl))
            {
                TempData["update"] = "<script>alert('updated')</script>";
                return RedirectToAction("Get_all");
            }
            else
            {
                TempData["update_error"] = "<script>alert('not updated')</script>";
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            if (dal.Delete(id))
            {
                TempData["delete"] = "<script>alert('Deleted')</script>";
            }
            return RedirectToAction("Get_all");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}