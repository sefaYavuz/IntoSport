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
        public int categorie_id { get; set; }
        public string naam { get; set; }
        public double prijs { get; set; }
        public double korting { get; set; }      
        public double voorraad { get; set; }
        public string afbeelding { get; set; }
        public string thumbnail { get; set; }

        public List<Category> categories { get; set; }

        public List<Detail> details { get; set; }

        public static List<Dictionary<string, object>> getAllProducts(string search = "")
        {
            Query query = new Query();
            query.Select("id, naam, prijs");
            query.From("product");

            if(search.Length > 0)
            {
                query.Where("naam LIKE '%" + search + "%'");
            }

            return query.Execute();
        }

    }
}
