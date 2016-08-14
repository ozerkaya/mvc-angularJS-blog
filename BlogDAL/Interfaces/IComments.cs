using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Interfaces
{
    interface IComments
    {
        int ID { get; set; }

        string Contact { get; set; }

        string Name { get; set; }

        string Comment { get; set; }

        DateTime Date { get; set; }
    }
}
