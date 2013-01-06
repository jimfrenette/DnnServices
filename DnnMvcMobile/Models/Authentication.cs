using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DnnMvcMobile.Models
{
    public class Authentication
    {
        public string DnnHttpAlias { get; set; }
        internal CookieContainer Cookies { get; set; }
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Username")] 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}