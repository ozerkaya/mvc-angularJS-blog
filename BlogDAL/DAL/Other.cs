using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogDAL.DAL
{
    public class Other
    {
        public int ID { get; set; }
        [Index("IX_CAPTION", 1, IsClustered = false, IsUnique = false)]
        [MaxLength(100)]
        public string Caption { get; set; }
        public decimal Fiyat { get; set; }
        public string Site { get; set; }
        public string Link { get; set; }
        public string Logo { get; set; }
    }
}