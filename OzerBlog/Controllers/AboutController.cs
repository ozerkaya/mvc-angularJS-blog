using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [OzerBlog.Interceptors.Logging.CustomLogger]
        public ActionResult Index()
        {
            ViewBag.title = "Hakkımda";
            return View();
        }
    }
}