using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDAL;
using BlogDAL.DAL;

namespace OzerBlog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [HttpGet]
        public ActionResult Index()
        {
            using (var db = new DBContext())
            {
                List<Posts> postList = new List<BlogDAL.DAL.Posts>();
                var posts = db.Posts.ToList().OrderByDescending(ok => ok.ID);
                foreach (var item in posts)
                {
                    postList.Add(item);
                }
                return View(postList);
            }

        }

        [HttpPost]
        public ActionResult Index(Posts model)
        {
            return View();
        }
    }
}