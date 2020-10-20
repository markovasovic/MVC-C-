using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int cathegory { get; set; }
        public string img { get; set; }

    }
}