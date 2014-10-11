using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IntoSport.Models
{
    public class KlantModel
    {


    }


    public class RegisterKlantModel
    {
        [Required]
        [Display(Name = "Email adres")]
        [EmailAddress(ErrorMessage = " The email adres is not valid")]
        public EmailAddressAttribute 

        [Required]
        public EmailAddressAttribute
    }
}