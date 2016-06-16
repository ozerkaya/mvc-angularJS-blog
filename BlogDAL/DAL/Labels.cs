using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class Labels
    {
        public int ID { get; set; }

        public string Label { get; set; }

        public Posts Post { get; set; }

        public int Post_ID { get; set; }

        public LabelTypes LabelTypes { get; set; }
        public int LabelTypes_ID { get; set; }
    }
}