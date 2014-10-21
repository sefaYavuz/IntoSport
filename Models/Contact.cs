using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;
using IntoSport.Helpers;

namespace IntoSport3.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Voer een geldige e-mail adres in!")]
        public string email { get; set; }

        [Required]
        public string topic { get; set; }

        [Required]
        public string message { get; set; }

    }
}