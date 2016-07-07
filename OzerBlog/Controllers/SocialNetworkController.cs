using BlogDAL.DAL;
using RepositoryBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Controllers
{
    public class SocialNetworkController : Controller
    {
        // GET: SocialNetwork
        public ActionResult Index()
        {
            return View();
        }

        
    }
}