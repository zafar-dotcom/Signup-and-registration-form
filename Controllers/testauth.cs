using Complete.DAL_Services;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class testauth : ControllerBase
    {
        [Route("GetData")]
        [HttpGet]
        public ActionResult GetData()
        {
            var users = Basic_auth_db.GetAlluser();

            return Ok(users);
            /*    string userName = Thread.CurrentPrincipal.Identity.Name;

                    if (userName != "")
                    {
                    var users = Basic_auth_db.GetAlluser();
                    return Ok(users);

                }
                    else
                    {
                    return Unauthorized();
                }
                */
        }

    }
}
