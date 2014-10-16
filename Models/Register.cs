using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;
using IntoSport.Helpers;

namespace IntoSport.Models
{
    public class Register : User
    {
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string voornaam { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string achternaam { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string straat { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]

        public override string huisnr { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string postcode { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string plaats { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public override string tel { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        [Remote("IsEmailAvailable", "Register", HttpMethod = "POST", ErrorMessage = "Het e-mail adres is al reeds in gebruik!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Voer een geldige e-mail adres in!")]
        public override string email { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Het wachtwoord moet minimaal 6 characters lang zijn")]
        public override string wachtwoord { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public string passwordrepeat { get; set; }

        public bool createAccount()
        {
            if (Insert())
            {
                return true;
            }
            return false;
        }

    }
}