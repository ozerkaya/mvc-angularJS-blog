using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogDAL.Interfaces;

namespace BlogDAL.DAL
{
    public sealed class Comments : IComments
    {

        public int ID
        {
            get;
            set;
        }

        public string Contact
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public Posts Post
        {
            get;
            set;
        }

        public int Post_ID
        {
            get;
            set;
        }
    }
}