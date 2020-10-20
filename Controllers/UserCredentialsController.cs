using Shop.Controllers.service_interaction_classes;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class UserCredentialsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            Crypt Crypt = new Crypt();
            Login_service service = new Login_service();

            string hashed_password = service.find_hashed_password(email);
            string new_hash = Crypt.Encrypt(password);


            if (hashed_password == new_hash)
            {
                Session["user_email"] = email;
                int id = service.find_user_id(email);
                Session["user_id"] = id;
                


                return RedirectToAction("Index", "Home");
                


            }
            return View();
        }





        public ActionResult Create_user_profile_page()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_user_profile(UserCredentials _usercredentials, string country)
        {


            services service = new services();
            if (ModelState.IsValid)
            {
                /// {GUID} creating unique ID for newly USER_information column in database and USER shoping cart for each individual user  .
                byte[] newGuid = Guid.NewGuid().ToByteArray();
                int id = BitConverter.ToInt16(newGuid, 0);

                service.create_shoping_cart_for_individual_user(id);
                service.Create_user_in_DB(_usercredentials, country, id);

                Session["successfully"] = "created successfully";
                return Redirect("Login");
            }

            else
            {
                return Redirect("Create_user_profile_page");
            }



        }




        public ActionResult logout() {

            Session["user_email"] = null;

            return RedirectToAction("index","Home");

        }


    }
}