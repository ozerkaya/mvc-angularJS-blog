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
                using (UnitOfWork work = new UnitOfWork())
                {
                    if (filterContext.Controller.GetType() == typeof(OzerBlog.Controllers.AboutController))
                    {
                        work.ViewLogsRepository.insert(new ViewLogs
                        {
                            Date = DateTime.Now,
                            Ip = HttpContext.Current.Request.UserHostAddress,
                            Title = "Hakkımda"
                        });
                    }
                    else if (filterContext.Controller.GetType() == typeof(OzerBlog.Controllers.ProjectController))
                    {
                        work.ViewLogsRepository.insert(new ViewLogs
                        {
                            Date = DateTime.Now,
                            Ip = HttpContext.Current.Request.UserHostAddress,
                            Title = "Projeler"
                        });
                    }
                    else if (filterContext.Controller.GetType() == typeof(OzerBlog.Controllers.SocialNetworkController))
                    {
                        work.ViewLogsRepository.insert(new ViewLogs
                        {
                            Date = DateTime.Now,
                            Ip = HttpContext.Current.Request.UserHostAddress,
                            Title = "Sosyal Ağ"
                        });
                    }
                    else if (filterContext.Controller.GetType() == typeof(OzerBlog.Controllers.BlogController))
                    {
                        var modelSingle = filterContext.Controller.ViewData.Model as Posts;
                        work.ViewLogsRepository.insert(new ViewLogs
                        {
                            Date = DateTime.Now,
                            Ip = HttpContext.Current.Request.UserHostAddress,
                            Title = modelSingle.title
                        });
                    }

                    work.Save();
                }
            }

            // ...
        }
    }
}