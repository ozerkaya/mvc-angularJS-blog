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
                var posts = db.Posts.Include("Label").ToList().OrderByDescending(ok => ok.ID);
                foreach (var item in posts)
                {
                    string itemText = GeneratePostFontText(item.content);
                    item.content = itemText;
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

        public ActionResult SinglePost(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Blog");
            }
            using (var db = new DBContext())
            {
                Posts posts = db.Posts.Include("Label").FirstOrDefault(ok => ok.ID == id);
                return View(posts);
            }

        }

        private string GeneratePostFontText(string text)
        {
            if (text.Length <= 500)
            {
                return text + "...";
            }
            else
            {
                string returnText = String.Empty;
                for (int i = 0; i <= 500; i++)
                {
                    returnText = returnText + text.Substring(i, 1);
                    if (i == 500)
                    {
                        int j = i;
                        while (true)
                        {
                            if (text.Substring(j, 1) == " ")
                            {
                                returnText = returnText + "...";
                                break;
                            }
                            else
                            {
                                returnText = returnText + text.Substring(j, 1);
                                j++;
                            }

                        }

                    }
                }


                return returnText;
            }
        }
    }
}