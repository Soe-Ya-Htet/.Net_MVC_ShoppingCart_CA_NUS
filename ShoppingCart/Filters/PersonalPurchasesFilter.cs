using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Filters
{
    // An Authorization Filter to guard the ViewPurchases view such that a
    // user can only view his/her own purchases
    public class PersonalPurchasesFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext context)
        {
            string sessionId = HttpContext.Current.Request["sessionId"];

            if (!Session.IsActiveSessionId(sessionId))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Login" }
                    }
                );
            }
        }
    }
}