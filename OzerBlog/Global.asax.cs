using BlogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OzerBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            using (var db = new DBContext())
            {
                var theme = db.ThemeOptions.FirstOrDefault();
                Session["BlogBossName"] = theme.BlogBossName;
                Session["BlogBossTitle"] = theme.BlogBossTitle;
                Session["BlogFooterText"] = theme.BlogFooterText;
                Session["Username"] = "ADMIN";
            }
        }
    }
}
