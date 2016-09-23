using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzerBlog.Extensions;

namespace OzerBlog.Controllers
{
    public class ProjectController : Controller
    {        
        // GET: Project
        public ActionResult Index(int id=0)
        {
            ViewBag.title = "Projelerim";
            return View();
        }
    }
}