using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzerBlog.Models
{
    public class PostsGet
    {
        public List<Posts> Posts;

        public List<dictionary> Enums;
    }

    public class dictionary
    {
        public string key;

        public int value;
    }
}