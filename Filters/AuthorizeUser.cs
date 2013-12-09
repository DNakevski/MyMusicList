using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicList.Areas.User.Models;
using System.Web.Routing;


namespace MyMusicList.Filters
{
    public class AuthorizeUser : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool authorized = true;

            User user = (User)HttpContext.Current.Session["User"];

            if (user == null)
                authorized = false;


            if (!authorized)
            { 
                RouteValueDictionary route = new RouteValueDictionary{{"Controller","Login"}, {"Action","Login"}};
                filterContext.Result = new RedirectToRouteResult(route);
            }

            this.OnActionExecuting(filterContext);
        }
    }
}