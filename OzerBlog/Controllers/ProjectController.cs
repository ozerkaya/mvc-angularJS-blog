using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzerBlog.Extensions;
using static OzerBlog.Helpers.HtmlGelpers;

namespace OzerBlog.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        [Compress]
        [OzerBlog.Interceptors.Logging.CustomLogger]
        public ActionResult Index(int id=0)
        {
            ViewBag.title = "Projelerim";
            return View();
        }
    }
}