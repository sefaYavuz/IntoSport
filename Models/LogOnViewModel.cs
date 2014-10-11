using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IntoSport.Models
{
    public class LogOnViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Niet een geldig email adres")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}