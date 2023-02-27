using Complete.DAL_Services;
using Complete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    public class Master_DetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(Applicant app)
        {
             Applicant_Experince_db.Add(app);
            return RedirectToAction("Index");

        }

    }
}
