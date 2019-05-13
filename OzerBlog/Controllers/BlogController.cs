using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDAL;
using RepositoryBL;
using RepositoryBL.Interfaces;
using static OzerBlog.Helpers.HtmlGelpers;

namespace OzerBlog.Controllers
{
    public class BlogController : Controller
    {
        [Compress]
        [HttpGet]

        public ActionResult Index(string title = "0")
        {
            
            using (BlogDAL.DBContext context = new DBContext())
            {                
                var timeBegin = DateTime.Now;
                var results = context.Labels.AsNoTracking().ToList();
                var timeEnd = DateTime.Now;
                TimeSpan fark = timeEnd - timeBegin;
            }

            using (BlogDAL.DBContext context = new DBContext())
            {
                var timeBegin = DateTime.Now;
                var results = context.Labels.ToList();
                var timeEnd = DateTime.Now;
                TimeSpan fark = timeEnd - timeBegin;
            }

            using (UnitOfWork work = new UnitOfWork())
            {
                var postList = new List<Posts>();
                var posts = new List<Posts>();
                if (title == "0")
                {
                    posts = work.PostsRepository.list("Label").OrderByDescending(ok => ok.ID).ToList();
                }
                else
                {
                    title = title.Replace("-", " ");
                    List<int> labelIds = work.LabelsRepository.listByWhere(ok => ok.LabelTypes.Key == title).Select(ok => ok.Post_ID).ToList();
                    posts = work.PostsRepository.listByWhere(ok => labelIds.Contains(ok.ID)).OrderByDescending(ok => ok.ID).ToList();
                }

                foreach (var item in posts)
                {
                    string itemText = GeneratePostFontText(item.content);
                    item.PageViewCount = work.ViewLogsRepository.countByWhere(ok => ok.Title == item.title);
                    item.CommentCount = work.CommentRepository.countByWhere(ok => ok.Post_ID == item.ID);
                    item.content = itemText;
                    postList.Add(item);
                }
                ViewBag.title = "Full Stack Software Developer";
                return View(postList);
            }
        }

        [Compress]
        [HttpPost]
        public ActionResult Index(Posts model)
        {
            return View();
        }

        [Compress]
        [OzerBlog.Interceptors.Logging.CustomLogger]
        public ActionResult SinglePost(string title = "")
        {
            if (title == string.Empty)
            {
                return RedirectToAction("Index", "Blog");
            }
            using (UnitOfWork work = new UnitOfWork())
            {
                title = title.Replace("-", " ");
                Posts post = work.PostsRepository.find(ok => ok.title == title, "Label").FirstOrDefault();
                ViewBag.title = post.title;
                return View(post);
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
                                returnText = returnText.Substring(0, returnText.Length - 1) + "...";
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