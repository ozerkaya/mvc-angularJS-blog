using BlogDAL;
using BlogDAL.DAL;
using OzerBlog.Helpers;
using OzerBlog.Models;
using RTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Login"] == "True")
            {
                return RedirectToAction("AdminMenu", "Admin");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            string sha256 = Security.sha256_hash(model.password);
            using (var connection = new DBContext())
            {
                if (connection.Users.Count(ok => ok.username == model.username && ok.password == sha256) > 0)
                {
                    Session["Username"] = model.username;
                    Session["Login"] = "True";
                    return RedirectToAction("AdminMenu", "Admin");
                }
                else
                {
                    Session["Login"] = "False";
                    model.loginMessage = "Kullanıcı Adı yada Şifre Hatalı!";
                    return View(model);
                }
            }

        }

        [HttpGet]
        public ActionResult AdminMenu()
        {
            using (var db = new DBContext())
            {
                return View(db.ThemeOptions.FirstOrDefault());
            }

        }

        [HttpPost]
        public ActionResult AdminMenu(ThemeOptions options)
        {
            using (var db = new DBContext())
            {
                var theme = db.ThemeOptions.FirstOrDefault(ok => ok.ID == options.ID);
                theme.BlogBossName = options.BlogBossName;
                theme.BlogBossTitle = options.BlogBossTitle;
                theme.BlogFooterText = options.BlogFooterText;
                theme.BlogHeaderPhoto = options.BlogHeaderPhoto;
                db.SaveChanges();

                Session["BlogBossName"] = options.BlogBossName;
                Session["BlogBossTitle"] = options.BlogBossTitle;
                Session["BlogFooterText"] = options.BlogFooterText;

                return View(theme);
            }

        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Pages()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Posts(int id=0)
        {
            using (var db = new DBContext())
            {
                PostView model = new PostView
                {
                    post = new Posts(),
                    postList = db.Posts.ToList().OrderByDescending(ok => ok.ID)
                };
                if (id!=0)
                {
                    var currentPost = db.Posts.FirstOrDefault(ok => ok.ID == id);
                    model.post = currentPost;
                }
                richEditorCreate(model.post.content);
                return View(model);
            }

        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Posts(PostView model, string Editor1)
        {
            using (var db = new DBContext())
            {
                Posts postModel = new BlogDAL.DAL.Posts
                {
                    content = Editor1,
                    title = model.post.title
                };
                model.post.content = Editor1;
                db.Posts.Add(postModel);
                db.SaveChanges();
                model.postList = db.Posts.ToList().OrderByDescending(ok => ok.ID);
            }
            richEditorCreate(string.Empty);
            return View(model);
        }

        public ActionResult PostsEdit(int id)
        {

            return View();
        }

        public void richEditorCreate(string content)
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            Editor1.Height = 500;
            Editor1.LoadFormData(content);
            Editor1.MvcInit();
            ViewBag.Editor = Editor1.MvcGetString();
        }
    }
}