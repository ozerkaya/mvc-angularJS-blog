using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzerBlog.Models
{
    public class Login
    {
        
        public string username { get; set; }
        public string password { get; set; }
        public string loginMessage { get; set; }
    }

   
}