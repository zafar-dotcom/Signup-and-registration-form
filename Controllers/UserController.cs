using Complete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    public class UserController : Controller
    {   
        DAL dal= new DAL();
        public IActionResult Getalluser()
        {
            return View(dal.GetEmployee());
        }
    }
}
