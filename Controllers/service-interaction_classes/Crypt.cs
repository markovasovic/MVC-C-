using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Shop.Controllers.service_interaction_classes
{
    public class Crypt
    {


        /// create hash method

        public string Encrypt(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var b = md5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password));
            string encrypt = Convert.ToBase64String(b);

            return encrypt;
        }


    }
}