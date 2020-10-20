using Shop.Controllers.service_interaction_classes;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class OrdersController : Controller
    {
        services services = new services();

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult InsertOrder()
        {
            int id = (int)Session["user_id"];
            List<Cart_Item> List = services.shoping_card_item(id);
            return View();
        }

    }
}