using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OzerBlog.Extensions
{
    public static class RazorHelpers
    {
        public static MvcHtmlString Table(this HtmlHelper htmlHelper, IEnumerable<object> model,
            string tableClass = "table table-bordered",
            string responsiveColumn = "Table Values",
            int widthPercent = 100,
            List<string> exculudeList = null)
        {
            StringBuilder returnValue = new StringBuilder();
            StringBuilder tdAll = new StringBuilder();
            StringBuilder tdOdd = new StringBuilder();

            if (model.Count() > 0)
            {
                PropertyInfo[] properties = model.FirstOrDefault().GetType().GetProperties();


                returnValue.AppendLine("<TABLE class='" + tableClass + "' width='" + widthPercent + "%'>");
                returnValue.AppendLine("<THEAD>");
                returnValue.AppendLine("<TR>");
                returnValue.AppendLine("<TH class='show hide'>" + responsiveColumn);
                returnValue.AppendLine("</TH>");
                foreach (var item in properties)
                {
                    if (exculudeList.IndexOf((string)item.Name) == -1)
                    {
                        returnValue.AppendLine("<TH>" + (string)item.Name.ToUpper() + "</TH>");
                    }
                }
                returnValue.AppendLine("</TR>");
                returnValue.AppendLine("</THEAD>");
                returnValue.AppendLine("<TBODY>");

                foreach (var item in model)
                {
                    returnValue.AppendLine("<TR>");
                    foreach (var modelValue in item.GetType().GetProperties())
                    {
                        if (exculudeList.IndexOf((string)modelValue.Name) == -1)
                        {
                            tdAll.AppendLine("<TD>" + Convert.ToString(modelValue.GetValue(item) ?? ""));
                            tdAll.AppendLine("</TD>");

                            tdOdd.AppendLine((string)modelValue.Name + " : " + Convert.ToString(modelValue.GetValue(item) ?? ""));
                            tdOdd.AppendLine("<br>");
                        }
                    }
                    returnValue.AppendLine("<TD class='show hide'>");
                    returnValue.AppendLine(string.Join(string.Empty, tdOdd));
                    returnValue.AppendLine("</TD>");
                    returnValue.AppendLine(string.Join(string.Empty, tdAll));
                    returnValue.AppendLine("</TR>");
                }

                returnValue.AppendLine("</TBODY>");
                returnValue.AppendLine("</TABLE>");
            }
            return new MvcHtmlString(string.Join(string.Empty, returnValue));
        }









        public static MvcHtmlString EncodedLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    else
                    {
                        queryString = "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }
            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }
            string link = Encrypt("/" + controllerName + "/" + actionName + "/" + queryString);
            link = "<a href='/EncodeUrl/?url=" + link + "'>" + linkText + "</a>";
            return new MvcHtmlString(link);
        }

        private static string Encrypt(string plainText)
        {
            string key = "www.ozerkaya.info";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
    }
}