using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace LtxMvcUtil.Web
{
    /// <summary>
    /// Adding the BasicAuthentication attribute tries to look for the AuthUser and AuthPassword keys in your Web.config, 
    /// and matches this with the value taken from the Authorization header in your Request object.
    /// </summary>
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The event coupled to IHttpAction method at which point authorization is enforced
        /// </summary>
        /// <param name="actionContext">Context object provided by the IHttpAction method</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                //Ensure that these values are present in your Web.config file; exceptions will be thrown otherwise
                if (username == ConfigurationManager.AppSettings["AuthUser"] &&
                    password == ConfigurationManager.AppSettings["AuthPword"])
                {
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(username), new string[] { });

                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
