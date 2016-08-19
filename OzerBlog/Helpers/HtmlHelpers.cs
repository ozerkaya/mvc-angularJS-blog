using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Helpers
{
    public static class HtmlGelpers
    {
        public static MvcHtmlString GetPageTitle(this HtmlHelper helper)
        {
            return new MvcHtmlString("Ozer KAYA" + " | " + helper.ViewContext.ViewBag.title ?? string.Empty);
        }

    }
}