using BlogDAL;
using BlogDAL.DAL;
using RepositoryBL;
using RepositoryBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Interceptors
{
    public class Logging
    {
        public class CustomLoggerAttribute : ActionFilterAttribute
        {
            SimpleRepo<Posts> repo = new SimpleRepo<BlogDAL.DAL.Posts>(new DBContext());
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var model = filterContext.Controller.ViewData.Model as IEnumerable<object>;
                using (UnitOfWork work = new UnitOfWork())
                {
                    if (model == null)
                    {
                        var modelSingle = filterContext.Controller.ViewData.Model as Posts;
                        work.ViewLogsRepository.insert(new ViewLogs
                        {
                            Date = DateTime.Now,
                            Ip = HttpContext.Current.Request.UserHostAddress + " - " + HttpContext.Current.Request.UserAgent,
                            Post_ID = modelSingle.ID
                        });
                    }
                    else
                    {

                        foreach (var item in model)
                        {
                            if (item.GetType() == typeof(Posts))
                            {
                                var modelValues = item as Posts;

                                work.ViewLogsRepository.insert(new ViewLogs
                                {
                                    Date = DateTime.Now,
                                    Ip = HttpContext.Current.Request.LogonUserIdentity.Name,
                                    Post_ID = modelValues.ID
                                });
                            }
                        }
                    }
                    work.Save();
                }
            }

            // ...
        }
    }
}