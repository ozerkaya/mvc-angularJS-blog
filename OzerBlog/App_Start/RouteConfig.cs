using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace OzerBlog
{
    public class RouteConfig
    {
        private class EncryptedRoute : Route, IRequiresSessionState
        {
            RouteCollection routes = new RouteCollection();
            public EncryptedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
                : base(url, defaults, routeHandler)
            {
                RegisterRoutesInternal(routes);
            }

            public override RouteData GetRouteData(HttpContextBase httpContext)
            {
                string encodedUrl = httpContext.Request.RawUrl;
                string[] splitUrl = encodedUrl.Split(new string[] { "?url=" }, StringSplitOptions.None);
                string url = string.Empty;
                RouteData rd = new RouteData();
                if (splitUrl.Count() > 1)
                {
                    url = Decrypt(splitUrl[1]).Replace("?id=", "");
                    rd = routes.GetRouteData(new HttpContextInjector(HttpContext.Current, new HttpRequestInjector(HttpContext.Current.Request, "~" + url)));
                }
                else
                {
                    rd = routes.GetRouteData(new HttpContextInjector(HttpContext.Current, new HttpRequestInjector(HttpContext.Current.Request, "~"+splitUrl[0])));
                }


                return rd;
            }

            private string Decrypt(string encryptedText)
            {
                string key = "www.ozerkaya.info";
                byte[] DecryptKey = { };
                byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
                byte[] inputByte = new byte[encryptedText.Length];
                DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByte = Convert.FromBase64String(encryptedText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByte, 0, inputByte.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }

            private sealed class HttpContextInjector : HttpContextWrapper
            {
                private HttpRequestBase _request;

                public HttpContextInjector(HttpContext httpContext, HttpRequestBase request)
                    : base(httpContext)
                {
                    _request = request;
                }

                public override HttpRequestBase Request
                {
                    get { return _request; }
                }
            }

            private sealed class HttpRequestInjector : HttpRequestWrapper
            {
                private string _appRelativeCurrentExecutionFilePath;

                public HttpRequestInjector(HttpRequest httpRequest, string appRelativeCurrentExecutionFilePath)
                    : base(httpRequest)
                {
                    _appRelativeCurrentExecutionFilePath = appRelativeCurrentExecutionFilePath;
                }

                public override string AppRelativeCurrentExecutionFilePath
                {
                    get { return _appRelativeCurrentExecutionFilePath; }
                }
            }


        }

        public static void RegisterRoutesInternal(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.asmx/{*pathInfo}");

            //routes.MapRoute(
            //    name: "EncodeUrl",
            //    url: "EncodeUrl/{url}",
            //    defaults: new { controller = "Blog", action = "Index", url = UrlParameter.Optional }
            //);

            routes.MapRoute(
               name: "SinglePost",
               url: "{controller}/{action}/{title}",
               defaults: new { controller = "Blog", action = "Index", title = UrlParameter.Optional }
           );

        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.Add(new EncryptedRoute("EncodeUrl/{url}", null, null));
            RegisterRoutesInternal(routes);
        }



    }
}
