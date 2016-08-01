using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJs_BasicCRUD.Models
{
    public class Books
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Operation { get; set; }

    }
}