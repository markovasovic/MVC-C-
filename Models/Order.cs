using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {


        public string orderID { get; set; }
        public int user_id { get; set; }
        public int articles { get; set; }
        public int totalamount { get; set; }
        public DateTime Date_of_Purashe { get; set; }


    }
}