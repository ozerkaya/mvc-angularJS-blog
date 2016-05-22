using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzerBlog.Models
{
    public class PostView
    {
        public Posts post { get; set; }
        public IEnumerable<Posts> postList { get; set; }
    }
}