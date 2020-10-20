using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class UserCredentials
    {

        public int id { get; set; }
        [Required]
        [Display(Name = "First and last name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Display(Name = "Adress")]
        public string Bilding_adress { get; set; }
        public int User_shoping_card { get; set; }



    }
}