using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Controllers.service_interaction_classes;
using Shop.Models;


namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        


        public ActionResult Index(string input1, string input2, string category)
        {
            
            services services = new services();
           
            var products = services.ReadALL();
            var products_conditions = services.ReadALLbyInputConditions(input1, input2, category);
           
            if (Request.HttpMethod == "POST")
            {
                return View(products_conditions);
            }
            else
            {
                services serv = new services();
                if (Session["user_id"]!= null)
                {
                    int id_ = (int)Session["user_id"];
                    int result = serv.count(id_);
                    Session["count"] = result;
                    
                }
                return View(products);
            }




        }




        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }






        public ActionResult Product_page(int? id)
        {
            services services = new services();
            int _id = Convert.ToInt32(id);
            List<Product> product = services.ReadProductbyID(_id);
            return View(product);


        }



        [HttpPost]
        public ActionResult Insert_product(int? product_id, string quantity, int price)
        {
            if (Session["user_email"] != null)
            {
                int user_id = (int)Session["user_id"];
                int int_quantity = Convert.ToInt32(quantity);
                int product = Convert.ToInt32(product_id);
                var multiply = (price * int_quantity);
                services serv = new services();
                serv.Inser_product_IN_Shoping_cart(user_id, product, int_quantity, multiply);
                Session["product_added"] = "product added in yours basket";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Login", "UserCredentials");

        }





        [HttpPost]
        public ActionResult AddProduct(Product _product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("INSERT INTO [dbo].[Product] VALUES(@name, @price, @description, @category, @img);", conn);
            comm.Parameters.AddWithValue("@name", _product.name);
            comm.Parameters.AddWithValue("@price", _product.price);
            comm.Parameters.AddWithValue("@description", _product.description);
            comm.Parameters.AddWithValue("@category", _product.cathegory);
            comm.Parameters.AddWithValue("@img", _product.img);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Contact");
        }



        public ActionResult ShopingCartList()
        {
            services services = new services();
            int id = Convert.ToInt32(Session["user_id"]);
            List<Cart_Item> list = services.shoping_card_item(id);
            Session["total"] = services.Total_amount(id);
           
            if (Session["user_email"] != null)
            {
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "UserCredentials");
            }




        }



        public ActionResult DeleteShopingCart_prod(int? id)
        {


            services services = new services();
            int user_id = Convert.ToInt32(Session["user_id"]);
            services.DeleteShopingCartItem(id, user_id);

            //var total = services.TotalValue(user_id);

            return RedirectToAction("ShopingCartList");

        }



        
        
      



    }
}