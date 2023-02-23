
using Complete.Models;
using Complete.DAL_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Complete.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class BasicAuthController : Controller
    {
        [Route("GetData")]
        //  [BasicAuthentication]
        [Authorize]

        public List<Basic_auth_tbluser_model> GetData()
        {
            // var users= Basic_auth_db.GetAlluser();
            return Basic_auth_db.GetAlluser();
            //  return Ok(users);

            // Do something with userName

        }
        [Route("getdatabyid")]
        [HttpGet]
        public IActionResult GetDatabyid(int id)
        {
            var user = Basic_auth_db.Get_user_at(id);
            return Ok(user);

        }
        [Route("adduser")]
        [HttpPost]
        public IActionResult AddUser(Basic_auth_tbluser_model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid model");
            }
            bool cond = Basic_auth_db.Insert_User(model);
            if (cond == true)
                return Ok();
            else
                return BadRequest("Not inserted");

        }
        [Route("update")]
        [HttpPut]
        public IActionResult Update(Basic_auth_tbluser_model user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid model");
            }
            bool cond = Basic_auth_db.Update(user);
            if (cond == true)
                return Ok();
            else
                return NotFound();
        }
        [Route("delete")]
        [HttpDelete]
        public IActionResult Delelte(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid model");
            }

            bool cond = Basic_auth_db.Delete(id);
            if (cond == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
