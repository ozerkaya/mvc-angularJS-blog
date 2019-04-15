using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class Posts
    {
        public int ID { get; set; }
        public string title { get; set; }

        public string content { get; set; }

        public IList<Labels> Label { get; set; }

        public IList<Comments> Comment { get; set; }

        public DateTime date { get; set; }

        [NotMapped]
        public int PageViewCount { get; set; }
        [NotMapped]
        public int CommentCount { get; set; }

    }
}