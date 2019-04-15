using System;
using System.Collections.Generic;
using System.IO.Compression;
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

        public class CompressAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var encodingAccept = filterContext.HttpContext.Request.Headers["Accept-Encoding"];

                if (string.IsNullOrEmpty(encodingAccept))
                    return;

                encodingAccept = encodingAccept.ToLowerInvariant();

                var response = filterContext.HttpContext.Response;

                if (encodingAccept.Contains("gzip"))
                {
                    response.AddHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (encodingAccept.Contains("deflate"))
                {
                    response.AddHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }

            }

        }
    }
}