using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PBL3_2.Attribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check the authorization logic
            bool isAuthorized = false; // Your authorization logic here

            if (!isAuthorized)
            {
                // Redirect to the Unauthorized action within the Error controller
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "Unauthorized" })
                );
            }
        }
    }

}