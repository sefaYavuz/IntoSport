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

        [Required]
        public override string email { get; set; }
        [Required]
        public override string wachtwoord { get; set; } 
    }
}