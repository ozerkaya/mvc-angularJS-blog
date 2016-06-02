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
        public ActionResult Posts()
        {
            return View();

        }

        public ActionResult PostsGet()
        {
            using (var db = new DBContext())
            {
                var postList = db.Posts.ToList().OrderByDescending(ok => ok.ID);
                return Json(postList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PostsEdit(int id)
        {
            using (var db = new DBContext())
            {
                Posts post = db.Posts.FirstOrDefault(ok => ok.ID == id);
                return Json(post, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PostDelete(int id)
        {
            using (var db = new DBContext())
            {
                var post = db.Posts.FirstOrDefault(ok => ok.ID == id);
                db.Posts.Attach(post);
                db.Posts.Remove(post);
                db.SaveChanges();
                var postList = db.Posts.ToList().OrderByDescending(ok => ok.ID);
                return Json(postList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}