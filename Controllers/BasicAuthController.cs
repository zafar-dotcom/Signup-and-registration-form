using Complete.DAL_Services;
using Complete.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicAuthController : ControllerBase
    {
        DAL obj=new DAL();
        [BasicAuthenticationAttribute]
        [Route("getemployee")]
       
        public List<UserModel> Get_Emplyee()
        {
            var emp=obj.GetEmployee();
            return emp;
        }

    }
}
