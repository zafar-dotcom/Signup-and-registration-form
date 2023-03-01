using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Complete.DAL_Services
{
    public class BasicAuthenticationAttribute :AuthorizationFilterAttribute
    {
      
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
            }
            else
            {
                try
                {
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    //username:password base64 encoded
                    //admin:password

                    string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] usernamepassword = decodedAuthToken.Split(':');

                    string username = usernamepassword[0];
                    string password = usernamepassword[1];

                    if (ValidateUser.Verfify(username, password))
                    {
                        //var userDetails = ValidateUser.GetUserDetails(username, password);
                        //var identity = new GenericIdentity(username);
                        //identity.AddClaim(new Claim(ClaimTypes.Name, userDetails.UserName));
                        //identity.AddClaim(new Claim(ClaimTypes.Email, userDetails.Email));
                        //identity.AddClaim(new Claim("Id", Convert.ToString(userDetails.Id)));

                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(username),null);

                        Thread.CurrentPrincipal = principal;
                        //if (HttpContext.Current != null)
                        //{
                        //    HttpContext.Current.User = principal;
                        //}
                        //else
                        //{
                        //    actionContext.Response = actionContext.Request
                        //        .CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
                        //}
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invaild Credentials");
                    }
                }
                catch (Exception)
                {

                    actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.InternalServerError, " InternalServerError - Please try after sometime");
                }

            }

        }

    }
}
