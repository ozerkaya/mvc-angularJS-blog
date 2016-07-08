using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Interceptors
{
    public class Logging
    {
        public class CustomLoggerAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);

                // Here goes your logic



                var aa = "aaaax";
                var bb = "ddd";
            }

            // ...
        }
    }
}