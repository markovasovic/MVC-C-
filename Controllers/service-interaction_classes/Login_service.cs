using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;

namespace Shop.Controllers.service_interaction_classes
{
    public class Login_service
    {

        /// read hashed password by inpuit email and return it to controller.
        public string find_hashed_password(string email)
        {
            StringBuilder hashed_password = new StringBuilder();
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select [password] from [dbo].[user_credentials] where [email] = '" + email + "'  ", conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                hashed_password.Append(reader["password"].ToString());

            }
            reader.Close();
            conn.Close();
            if (hashed_password != null)
            {
                return hashed_password.ToString();
            }
            return "notfound";

        }


        int id;
        public int find_user_id(string email)
        {

            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mydb;Integrated Security=True");
            SqlCommand comm = new SqlCommand("select id from [dbo].[user_credentials] where [email] = '" + email + "'  ", conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["id"];

            }
            reader.Close();
            conn.Close();

            return id;



        }



    }
}