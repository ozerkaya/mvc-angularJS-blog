using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzerBlog.Models
{
    public class PostEditTake
    {
        public Posts Post { get; set; }

        public List<label> Labels { get; set; }
    }

    public class label
    {
        public string key;

        public int value;
    }
}