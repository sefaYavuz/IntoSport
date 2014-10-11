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
        public string Email { get; set;}

        [Required]
        public string Password { get; set;}

        [Required]
        public string ConfirmPassword { get; set;}

        [Required]
        public string Voornaam { get; set;}

        [Required]
        public string Achternaam { get; set;}

        [Required]
        public string Adres { get; set;}

        [Required]
        public string Postcode {get; set;}

        [Required]
        public string Plaats {get; set;}

        [Required]
        public string TelNr {get; set;}


    }
}