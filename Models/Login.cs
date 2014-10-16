using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntoSport.Helpers;
using System.ComponentModel.DataAnnotations;

namespace IntoSport.Models
{
    public class Login : User
    {

        [Required(ErrorMessage = "- Email is een verplicht veld!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "- Voer een geldige e-mail adres in!")]

        public override string email { get; set; }
        [Required(ErrorMessage = "- Wachtwoord is een verplicht veld!")]
        public override string wachtwoord { get; set; } 
    }
}