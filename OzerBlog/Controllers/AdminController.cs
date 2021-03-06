﻿using BlogDAL;
using BlogDAL.DAL;
using OzerBlog.Helpers;
using OzerBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryBL;
using BlogDAL;
using RepositoryBL.Interfaces;
using System.Web.Security;

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
                ViewBag.title = "Admin Kontrol";
                return View();
            }

        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            string sha256 = Security.sha256_hash(model.password);

            using (UnitOfWork work = new UnitOfWork())
            {
                if (work.UserRepository.countByWhere(ok => ok.username == model.username && ok.password == sha256) > 0)
                {
                    Session["Username"] = model.username;
                    Session["Login"] = "True";
                    FormsAuthentication.SetAuthCookie(model.username, true);
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
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["Login"] = "False";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AdminMenu()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return View(work.ThemeOptionsRepository.getFirstOrDefault());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AdminMenu(ThemeOptions options)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var theme = work.ThemeOptionsRepository.findById(options.ID);
                theme.BlogBossName = options.BlogBossName;
                theme.BlogBossTitle = options.BlogBossTitle;
                theme.BlogFooterText = options.BlogFooterText;
                theme.BlogHeaderPhoto = options.BlogHeaderPhoto;

                work.ThemeOptionsRepository.update(theme);
                work.Save();

                Session["BlogBossName"] = options.BlogBossName;
                Session["BlogBossTitle"] = options.BlogBossTitle;
                Session["BlogFooterText"] = options.BlogFooterText;

                return View(theme);
            }

        }

        [Authorize]
        public ActionResult Users()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pages()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Posts()
        {
            return View();

        }

        [HttpGet]
        [Authorize]
        public ActionResult Logs()
        {
            return View();

        }

        [Authorize]
        public ActionResult PostsGet()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                PostsGet PostsGet = new PostsGet
                {
                    Posts = work.PostsRepository.list().OrderByDescending(ok => ok.ID).ToList(),
                    Enums = new List<dictionary>()
                };

                foreach (var item in work.LabelTypesRepository.list())
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

        [Authorize]
        public ActionResult PostsEdit(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                PostEditTake model = new PostEditTake
                {
                    Post = work.PostsRepository.findById(id)
                };

                model.Labels = new List<label>();
                foreach (var item in work.LabelsRepository.listByWhere(ok => ok.Post_ID == id))
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

        [Authorize]
        public ActionResult PostDelete(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.PostsRepository.delete(ok => ok.ID == id);
                work.Save();
                return Json(work.PostsRepository.list(), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SaveEditPost(int ID, string Content, string Title, string Labels, string labelsText)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                Labels = Labels.Substring(0, Labels.Length - 1);
                var labelList = Labels.Split(',');
                if (ID == 0)
                {
                    Posts post = new Posts
                    {
                        content = KarakterDuzenle(Content),
                        title = KarakterDuzenle(Title),
                        date = DateTime.Now
                    };

                    post.Label = new List<Labels>();
                    foreach (var item in labelList)
                    {
                        int labelID = Convert.ToInt32(item);
                        post.Label.Add(new Labels
                        {
                            LabelTypes_ID = labelID,
                            Label = work.LabelTypesRepository.findById(labelID).Key

                        });
                    }
                    work.PostsRepository.insert(post);
                    work.Save();
                }
                else
                {
                    Posts post = work.PostsRepository.findById(ID, "Label");
                    post.content = KarakterDuzenle(Content);
                    post.title = KarakterDuzenle(Title);
                    post.date = DateTime.Now;
                    work.LabelsRepository.removeRange(ok => ok.Post_ID == ID);

                    foreach (var item in labelList)
                    {
                        int labelID = Convert.ToInt32(item);
                        post.Label.Add(new Labels
                        {
                            LabelTypes_ID = labelID,
                            Label = work.LabelTypesRepository.findById(labelID).Key

                        });
                    }
                }

                work.Save();
                var postList = work.PostsRepository.list().Select(ok => new { ok.content, ok.ID, ok.title }).OrderByDescending(ok => ok.ID);
                return Json(postList, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult UsersGet()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return Json(work.UserRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult UsersEdit(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return Json(work.UserRepository.find(ok => ok.ID == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult UserDelete(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.UserRepository.deleteById(id);
                work.Save();
                return Json(work.UserRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SaveEditUser(int ID, string Username, string Password)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                if (ID == 0)
                {
                    User user = new User
                    {
                        username = Username,
                        password = Security.sha256_hash(Password)
                    };
                    work.UserRepository.insert(user);
                }
                else
                {
                    User user = work.UserRepository.findById(ID);
                    user.username = Username;
                    user.password = Security.sha256_hash(Password);
                }
                work.Save();
                var userList = work.UserRepository.list().OrderByDescending(ok => ok.ID);
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult AddNewLabel(string label)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.LabelTypesRepository.insert(new LabelTypes
                {
                    Key = label
                });
                work.Save();

                dictionary dic = new dictionary
                {
                    key = label,
                    value = work.LabelTypesRepository.listByWhere(ok => ok.Key == label).FirstOrDefault().ID,
                    check = false
                };

                return Json(dic, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SaveEditSocialNetwork(int platformID, string platform, string adress, string image, bool active)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                if (platformID == 0)
                {
                    SocialContacts contact = new SocialContacts
                    {
                        Active = active,
                        Address = adress,
                        Image = image,
                        Platform = platform
                    };
                    work.SocialContactsRepository.insert(contact);
                }
                else
                {
                    SocialContacts contact = work.SocialContactsRepository.findById(platformID);
                    contact.Active = active;
                    contact.Address = adress;
                    contact.Image = image;
                    contact.Platform = platform;
                }
                work.Save();
                return Json(work.SocialContactsRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SocialNetworkGet()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return Json(work.SocialContactsRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult LogsGet()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var repo = work.ViewLogsRepository.list().OrderByDescending(ok => ok.ID);
                List<Logsget> logList = new List<Logsget>();
                foreach (var item in repo)
                {
                    Logsget logs = new Logsget
                    {
                        Date = item.Date.ToString(),
                        ID = item.ID,
                        Ip = item.Ip,
                        Title = item.Title
                    };
                    logList.Add(logs);
                }
                return Json(logList, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SocialNetworkDelete(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.SocialContactsRepository.deleteById(id);
                work.Save();
                return Json(work.SocialContactsRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SocialNetworkEdit(int id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return Json(work.SocialContactsRepository.find(ok => ok.ID == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult SocialNetwork()
        {
            return View();
        }

        public static string KarakterDuzenle(string metin)
        {
            string Duzenlenmis = metin;
            Duzenlenmis = Duzenlenmis.Replace("&#304;", "I");
            Duzenlenmis = Duzenlenmis.Replace("&#305;", "i");
            Duzenlenmis = Duzenlenmis.Replace("&#214;", "Ö");
            Duzenlenmis = Duzenlenmis.Replace("&#246;", "ö");
            Duzenlenmis = Duzenlenmis.Replace("&Ouml;", "Ö");
            Duzenlenmis = Duzenlenmis.Replace("&ouml;", "ö");
            Duzenlenmis = Duzenlenmis.Replace("&#220;", "Ü");
            Duzenlenmis = Duzenlenmis.Replace("&#252;", "ü");
            Duzenlenmis = Duzenlenmis.Replace("&Uuml;", "Ü");
            Duzenlenmis = Duzenlenmis.Replace("&uuml;", "ü");
            Duzenlenmis = Duzenlenmis.Replace("&#199;", "Ç");
            Duzenlenmis = Duzenlenmis.Replace("&#231;", "ç");
            Duzenlenmis = Duzenlenmis.Replace("&Ccedil;", "Ç");
            Duzenlenmis = Duzenlenmis.Replace("&ccedil;", "ç");
            Duzenlenmis = Duzenlenmis.Replace("&#286;", "G");
            Duzenlenmis = Duzenlenmis.Replace("&#287;", "g");
            Duzenlenmis = Duzenlenmis.Replace("&#350;", "S");
            Duzenlenmis = Duzenlenmis.Replace("&#351;", "s");
            return Duzenlenmis;
        }
    }
}