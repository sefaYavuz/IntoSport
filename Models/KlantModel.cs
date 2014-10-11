using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IntoSport.Models
{
    public class KlantModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string Postcode { get; set; }

        public string Adres { get; set; }

        public string Plaats { get; set; }

        public string TelNr { get; set; }
        


    }


    public class RegisterKlantModel
    {
        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        [Display(Name = "Email adres")]
        public string Email { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string Password { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        [Compare("Password", ErrorMessage = "Confirm password dose not match.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string Voornaam { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string Achternaam { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string Adres { get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string Postcode {get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string Plaats {get; set;}

        [Required(ErrorMessage = "please", AllowEmptyStrings = false)]
        public string TelNr {get; set;}


    }
}