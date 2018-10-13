using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Controllers
{
    public class OtherController : Controller
    {
        // GET: Other
        public JsonResult Index(string key)
        {
            using (BlogDAL.DBContext db = new BlogDAL.DBContext())
            {
                key = key.Replace(" - Google'da Ara", "")
                         .Replace(" - n11.com", "")
                         .Replace(" - GittiGidiyor", "");


                List<IDList> idList = new List<IDList>();
                List<otherTransfer> list = new List<otherTransfer>();
                List<Other> listitemDB = new List<Other>();
                string[] array = key.Split(' ');
                int i = 0;

                foreach (var item in array.ToList())
                {
                    if (item.Length > 1)
                    {
                        List<Other> listitem = db.Others.Where(ok => ok.Caption.Contains(item)).ToList();
                        listitemDB.AddRange(listitem);
                        foreach (var urun in listitem)
                        {

                            idList.Add(new IDList
                            {
                                ID = urun.ID,
                                Count = 1

                            });
                        }
                        i++;
                    }
                }

                int yari = i / 2;

                var result = idList.GroupBy(oz => new { oz.ID }).Select(oz => new { ID = oz.FirstOrDefault().ID, Count = oz.Sum(b => b.Count) }).ToList();

                foreach (var item in result.OrderByDescending(ok => ok.Count).ToList())
                {
                    if (item.Count > yari)
                    {
                        Other other = listitemDB.FirstOrDefault(ok => ok.ID == item.ID);
                        list.Add(new otherTransfer
                        {
                            Caption = other.Caption,
                            Count = item.Count,
                            Fiyat = other.Fiyat,
                            Link = other.Link,
                            Logo = other.Logo,
                            Site = other.Site
                        });
                    }
                }


                return Json(list.OrderByDescending(ok => ok.Count).Skip(0).Take(3).OrderBy(ok => ok.Fiyat).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}

public class IDList
{
    public int ID { get; set; }
    public int Count { get; set; }
}

public class otherTransfer : Other
{
    public int Count { get; set; }
}