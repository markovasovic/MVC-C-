using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Text;

namespace Shop.Controllers.service_interaction_classes
{
    public class services
    {






        /// <summary>
        ///  Reading all PRODUCTS from database and display on main (starting) web page 
        /// </summary>

        public List<Product> ReadALL()
        {

            List<Product> list_of_products = new List<Product>();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select id, name, price, description, category, img from Product ", conn);
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product();
                product.id = (int)reader["id"];
                product.name = (string)reader["name"];
                product.price = (decimal)reader["price"];
                product.description = (string)reader["description"];
                product.cathegory = (int)reader["category"];
                product.img = (string)reader["img"];
                list_of_products.Add(product);
            }
            reader.Close();
            conn.Close();

            return list_of_products;

        }




        /// <summary>
        ///  Reading all PRODUCTS from database by slider conditions 
        /// </summary>


        public List<Product> ReadALLbyInputConditions(string input1, string input2, string category)
        {
            decimal _input1 = Convert.ToDecimal(input1);
            decimal _input2 = Convert.ToDecimal(input2);
            int _category = Convert.ToInt32(category);
            List<Product> list_of_products = new List<Product>();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select id, name, price, description, category, img from Product where price BETWEEN '" + _input1 + "' AND '" + _input2 + "' and category = '" + _category + "' ", conn);
            comm.Parameters.AddWithValue("@input1", _input1);
            comm.Parameters.AddWithValue("@input2", _input2);
            comm.Parameters.AddWithValue("@category", _category);
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product();
                product.id = (int)reader["id"];
                product.name = (string)reader["name"];
                product.price = (decimal)reader["price"];
                product.description = (string)reader["description"];
                product.cathegory = (int)reader["category"];
                product.img = (string)reader["img"];
                list_of_products.Add(product);
            }
            reader.Close();
            conn.Close();

            return list_of_products;

        }






        // create profile of new user in data base !
        public void Create_user_in_DB(UserCredentials user, string country, int id)
        {
            Crypt crypt = new Crypt();

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("INSERT INTO user_credentials (id, username, password, email,country, city, bilding_adress, user_shoping_cart) VALUES (@id,  @username, @password, @email, @country, @city, @bilding_adress, @user_shoping_cart ) ", conn);
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@username", user.Username);
            comm.Parameters.AddWithValue("@password", crypt.Encrypt(user.Password));
            comm.Parameters.AddWithValue("@email", user.Email);
            comm.Parameters.AddWithValue("@country", country);
            comm.Parameters.AddWithValue("@city", user.City);
            comm.Parameters.AddWithValue("@bilding_adress", user.Bilding_adress);
            comm.Parameters.AddWithValue("@user_shoping_cart", id);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

        }




        // create unique shopingcart for newly created user !
        public void create_shoping_cart_for_individual_user(int guid)
        {

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand(" INSERT INTO user_shoping_cart (user_id) VALUES ('" + guid + "') ", conn);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }










        // find individual item by ID for PRODUCT information page
        public List<Product> ReadProductbyID(int id)
        {
            List<Product> list_of_products = new List<Product>();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select id, name, price, description, category, img from Product where id = @id ", conn);
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product();
                product.id = (int)reader["id"];
                product.name = (string)reader["name"];
                product.price = (decimal)reader["price"];
                product.description = (string)reader["description"];
                product.cathegory = (int)reader["category"];
                product.img = (string)reader["img"];
                list_of_products.Add(product);
            }
            reader.Close();
            conn.Close();

            return list_of_products;


        }



        /// Insert product in user shoping cart
        public void Inser_product_IN_Shoping_cart(int user_id, int product_id, int int_quantity, int multiply)
        {
            Crypt crypt = new Crypt();

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("INSERT INTO user_shoping_cart (user_id, product_id, quantity, multiply) VALUES (@user_id, @product_id, @quantity, @multiply) ", conn);
            comm.Parameters.AddWithValue("@user_id", user_id);
            comm.Parameters.AddWithValue("@product_id", product_id);
            comm.Parameters.AddWithValue("@quantity", int_quantity);
            comm.Parameters.AddWithValue("@multiply", multiply);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

        }





        /// user shoping card item list
        /// 
        public List<Cart_Item> shoping_card_item(int user_id)
        {
            List<Cart_Item> list_of_products = new List<Cart_Item>();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select user_shoping_cart.id as id, quantity, name, price, quantity, multiply from user_shoping_cart inner join dbo.Product on product_id = Product.id and user_id = '" + user_id+"' ", conn);


            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Cart_Item item = new Cart_Item();

                item.id = (int)reader["id"];
                item.name = (string)reader["name"];
                item.price = (decimal)reader["price"];
                item.quantity = (int)reader["quantity"];
                item.multiply = (int)reader["multiply"];
                list_of_products.Add(item);
            }
            reader.Close();
            conn.Close();

            return list_of_products;


        }


        /// Delete item from shoping cart
        /// 
        public void DeleteShopingCartItem(int? item_id, int user_id)
        {

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand(" delete  from user_shoping_cart where user_shoping_cart.id = '" + item_id + "'  and user_id = '" + user_id + "'  ", conn);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }


        ///// total value of all products in user shoping cart .



        public int Total_amount(int user_id)
        {
            int data = new int();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("SELECT SUM(multiply) as value FROM[mydb].[dbo].[user_shoping_cart] where  user_iD = '" + user_id + "' ", conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(reader.GetOrdinal("value")))
                {
                    data = (int)reader["value"];
                }
               
            }
            reader.Close();
            conn.Close();
            if (data > 0)
            {
                return data;
            }

            return 0;
        }

        

       

        public  void send_mail_to_seller(int id)
        {
            var myList = user_products_information_admin(id);
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("vasovacmarko@gmail.com");
                mail.To.Add("marko.vasovic@yahoo.com");
                mail.Subject = "order";
                
                StringBuilder sb2 = new StringBuilder();
                string sb1 = "<h1> list of purashed items for sending to the buyer location </h1><br>";
                sb2.Append(sb1);
                foreach (var item in myList)
                {
                    sb2.Append("<br><h3>item name:</h3> " + item.name+ " <br><h3>item price:</h3> " + item.price + " <br> <h3> item quantity:</h3> " + item.quantity + " <br><br>_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                }
                admin_order_mail user_info = new admin_order_mail();
                user_info = user_personal_information_admin(id);
                mail.Body = "<h1> Buyer personal information :</h1> <br> <h3>First and last name:</h3> " + user_info.name+ " <br> <h3>Bilding  adress :</h3> " + user_info.bilding_adress+ "  <br> <h3> City:</h3> " + user_info.city+ "   <br>  <h3> Country :</h3>" + user_info.country+ "  <br> <h3>Email:</h3> " + user_info.email+ " <br> ....................................................................................  " + sb2;
                mail.IsBodyHtml = true;
                

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("vasovacmarko@gmail.com", "markovasovic");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }



        }



        // user shiping information for admin email .

        public admin_order_mail user_personal_information_admin(int id)
        {
            admin_order_mail user_info = new admin_order_mail();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand(" SELECT username, country, email, city, bilding_adress FROM user_credentials WHERE id = '" + id+ "'", conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
               
                user_info.name = reader["username"].ToString();
                user_info.country = reader["country"].ToString();
                user_info.city = reader["city"].ToString();
                user_info.email = reader["email"].ToString();
                user_info.bilding_adress = reader["bilding_adress"].ToString();
                
            }
            reader.Close();
            conn.Close();
            return user_info;
           
           
        }

        public List<admin_order_mail_2> user_products_information_admin(int id)
        {
            List<admin_order_mail_2> lista = new List<admin_order_mail_2>();
            
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand(" SELECT [name],[quantity] ,[price] FROM [mydb].[dbo].[user_shoping_cart] join [mydb].[dbo].[Product] on product_id = [mydb].[dbo].[Product].id where user_id = "+id+"  ", conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                admin_order_mail_2 user_info = new admin_order_mail_2();
                user_info.name = reader["name"].ToString();
                user_info.quantity = reader["quantity"].ToString();
                user_info.price = Convert.ToString(reader["price"]);
                lista.Add(user_info);
            }
            reader.Close();
            conn.Close();
            return lista;


        }







        public int count(int user_id)
        {
            int data = new int();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select count(product_id) as article_counts from  user_shoping_cart where user_id = "+user_id+" ;", conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(reader.GetOrdinal("article_counts")))
                {
                    data = (int)reader["article_counts"];
                }

            }
            reader.Close();
            conn.Close();
            if (data > 0)
            {
                return data;
            }

            return 0;
        }

        public void Delete_items(int id) {
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("DELETE FROM user_shoping_cart WHERE user_id = "+ id+"  ", conn);
            conn.Open();
            comm.ExecuteScalar();
            conn.Close();
        }















    }


}
