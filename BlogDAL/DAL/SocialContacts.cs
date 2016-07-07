using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class SocialContacts
    {
        public int ID { get; set; }

        public string Platform { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }
    }
}