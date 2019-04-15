using BlogDAL.DAL;
using RepositoryBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OzerBlog.Helpers.HtmlGelpers;

namespace OzerBlog.Controllers
{
    public class SocialNetworkController : Controller
    {
        // GET: SocialNetwork
        [Compress]
        [OzerBlog.Interceptors.Logging.CustomLogger]
        public ActionResult Index()
        {
            ViewBag.title = "Sosyal Ağım";
            using (UnitOfWork work = new UnitOfWork())
            {
                return View(work.SocialContactsRepository.list()); 
            }
            
        }

        
    }
}