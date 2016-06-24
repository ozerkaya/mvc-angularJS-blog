using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDAL;
using RepositoryBL;
using RepositoryBL.Interfaces;

namespace OzerBlog.Controllers
{
    public class BlogController : Controller
    {
        SimpleRepo<Posts> repo = new SimpleRepo<BlogDAL.DAL.Posts>(new DBContext());
        // GET: Blog
        [HttpGet]
        public ActionResult Index()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return View(work.PostsRepository.list("Label").OrderByDescending(ok => ok.ID));
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
            using (UnitOfWork work = new UnitOfWork())
            {
                return View(work.PostsRepository.find(ok => ok.ID == id, "Label").FirstOrDefault());
            }

        }

        private string GeneratePostFontText(string text)
        {
            if (text.Length <= 250)
            {
                return text + "...";
            }
            else
            {
                string returnText = String.Empty;
                for (int i = 0; i <= 250; i++)
                {
                    returnText = returnText + text.Substring(i, 1);
                    if (i == 250)
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
                                returnText = returnText + text.Substring(j + 1, 1);
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