using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class ViewLogs
    {
        public int ID { get; set; }

        public string Ip { get; set; }

        public Posts Post { get; set; }

        public int Post_ID { get; set; }

        public DateTime Date { get; set; }
    }
}