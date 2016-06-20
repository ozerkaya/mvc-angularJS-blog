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

        public ActionResult LogOut()
        {
            Session["Login"] = "False";
            return RedirectToAction("Index", "Home");
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
                PostsGet PostsGet = new PostsGet
                {
                    Posts = db.Posts.OrderByDescending(ok => ok.ID).ToList(),
                    Enums = new List<dictionary>()
                };

                foreach (var item in db.LabelTypes.ToList())
                {
                    dictionary dic = new dictionary
                    {
                        key = item.Key,
                        value = item.ID,
                        check = false
                    };
                    PostsGet.Enums.Add(dic);
                }

                return Json(PostsGet, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PostsEdit(int id)
        {
            using (var db = new DBContext())
            {
                PostEditTake model = new PostEditTake
                {
                    Post = db.Posts.FirstOrDefault(ok => ok.ID == id)
                };

                model.Labels = new List<label>();
                foreach (var item in db.Labels.Where(ok => ok.Post_ID == id))
                {
                    model.Labels.Add(new label
                    {
                        key = item.Label,
                        value = item.LabelTypes_ID
                    });
                }

                model.Post.Label = null;
                return Json(model, JsonRequestBehavior.AllowGet);
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

        public ActionResult SaveEditPost(int ID, string Content, string Title, string Labels, string labelsText)
        {
            using (var db = new DBContext())
            {
                Labels = Labels.Substring(0, Labels.Length - 1);
                var labelList = Labels.Split(',');
                if (ID == 0)
                {
                    Posts post = new Posts
                    {
                        content = Content,
                        title = Title,
                        date = DateTime.Now
                    };

                    post.Label = new List<Labels>();
                    foreach (var item in labelList)
                    {
                        int labelID = Convert.ToInt32(item);
                        post.Label.Add(new Labels
                        {
                            LabelTypes_ID = labelID,
                            Label = db.LabelTypes.FirstOrDefault(ok => ok.ID == labelID).Key

                        });
                    }
                    db.Posts.Add(post);
                }
                else
                {
                    Posts post = db.Posts.Include("Label").FirstOrDefault(ok => ok.ID == ID);
                    post.content = Content;
                    post.title = Title;
                    post.date = DateTime.Now;
                    db.Labels.RemoveRange(db.Labels.Where(ok => ok.Post_ID == ID));

                    foreach (var item in labelList)
                    {
                        int labelID = Convert.ToInt32(item);
                        post.Label.Add(new Labels
                        {
                            LabelTypes_ID = labelID,
                            Label = db.LabelTypes.FirstOrDefault(ok => ok.ID == labelID).Key

                        });
                    }
                }

                db.SaveChanges();
                var postList = db.Posts.Select(ok => new { ok.content, ok.ID, ok.title }).OrderByDescending(ok => ok.ID).ToList();
                return Json(postList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UsersGet()
        {
            using (var db = new DBContext())
            {
                var userList = db.Users.ToList().OrderByDescending(ok => ok.ID);
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UsersEdit(int id)
        {
            using (var db = new DBContext())
            {
                User user = db.Users.FirstOrDefault(ok => ok.ID == id);
                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UserDelete(int id)
        {
            using (var db = new DBContext())
            {
                var user = db.Users.FirstOrDefault(ok => ok.ID == id);
                db.Users.Attach(user);
                db.Users.Remove(user);
                db.SaveChanges();
                var userList = db.Users.ToList().OrderByDescending(ok => ok.ID);
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveEditUser(int ID, string Username, string Password)
        {
            using (var db = new DBContext())
            {
                if (ID == 0)
                {
                    User user = new User
                    {
                        username = Username,
                        password = Security.sha256_hash(Password)
                    };
                    db.Users.Add(user);
                }
                else
                {
                    User user = db.Users.FirstOrDefault(ok => ok.ID == ID);
                    user.username = Username;
                    user.password = Security.sha256_hash(Password);
                }
                db.SaveChanges();
                var userList = db.Users.ToList().OrderByDescending(ok => ok.ID);
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}