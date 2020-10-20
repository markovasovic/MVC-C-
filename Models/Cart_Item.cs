using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Cart_Item
    {

        public int id { get; set; }

        public string name { get; set; }

        [Display(Name = "product image")]
        public string img { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        [Display(Name = "item total")]
        public int multiply { get; set; }

    }
}