using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class ThemeOptions
    {
        public int ID { get; set; }

        public string BlogBossName { get; set; }

        public string BlogBossTitle { get; set; }

        public string BlogFooterText { get; set; }

        public string BlogHeaderPhoto { get; set; }

    }
}