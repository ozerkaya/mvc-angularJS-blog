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
                    int count = work.LabelsRepository.countByWhere(ok => ok.LabelTypes_ID == item.ID);
                    menuList.Add(new Menu
                    {
                        title = item.Key + " (" + count.ToString() + ")",
                        link = item.Key.ToString()
                    });
                }
                return PartialView(menuList);
            }

        }
    }
}

