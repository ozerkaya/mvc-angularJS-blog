using BlogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzerBlog.Models;
using RepositoryBL.Interfaces;

namespace OzerBlog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _RightMenu()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                List<Menu> menuList = new List<Menu>();
                var labels = work.LabelTypesRepository.list();
                foreach (var item in labels)
                {
                    menuList.Add(new Menu
                    {
                        title = item.Key,
                        link = item.ID.ToString()
                    });
                }
                return PartialView(menuList);
            }

        }
    }
}

