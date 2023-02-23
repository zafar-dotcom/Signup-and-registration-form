using Complete.DAL_Services;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Complete.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
       {
            if (actionContext.Request.Headers.Authorization == null)
            {
               // actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "login failed");
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray =decodedAuthenticationToken.Split(':');          
                string uname = usernamePasswordArray[0];
                string pass = usernamePasswordArray[1];
                if (Basic_auth_db.Login(uname, pass))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(uname), null);
                    //var identity = new ClaimsIdentity(new[] {
                    //new Claim(ClaimTypes.Name, uname)
                    //});
                    //var principal = new ClaimsPrincipal(identity);
                    //Thread.CurrentPrincipal = principal;
                }
                else
                {
                  //  actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "login re failed");
                     actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }


    }
    
}
