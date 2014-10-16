using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Models
{
    public class Product 
    {
        public int id{get; set;}
        private int orderregel2_id{get; set;}
        private int categorie_id{get; set;}
        private string naam{get; set;}
        private double prijs{get; set;}
        private double prijs_oud{get; set;}
        private double hoeveelheid{get; set;}
        private string merk{get; set;}
        private string maat{get; set;}
        private string afbeelding{get; set;}
        private string thumbnail{get; set;}

    }
}
