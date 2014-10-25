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
        public string beschrijving { get; set; }
        public double prijs { get; set; }
        public int korting { get; set; }      
        public int voorraad { get; set; }
        public string afbeelding { get; set; }
        public string thumbnail { get; set; }

        public List<Category> categories { get; set; }

        public List<Detail> details { get; set; }

        public Product()
        {
            this.id = 0;
        }

        public Product(int productID)
        {
            Query query = new Query();
            query.Select("*");
            query.From("product");
            query.Where("id = " + productID);

            foreach(Dictionary<string, object> product in query.Execute())
            {
                this.id             = (int)product["id"];
                this.naam           = (string)product["naam"];
                this.beschrijving   = (string)product["beschrijving"];
                this.prijs          = (double)product["prijs"];
                this.korting        = (int)product["korting"];
                this.voorraad       = (int)product["voorraad"];
                this.afbeelding     = (string)product["afbeelding"];
                this.thumbnail      = (string)product["thumbnail"];
            }
        }

        public static int Insert(FormCollection collection)
        {
            Dictionary<string, object> data = new Dictionary<string,object>();
            data.Add("naam", collection["naam"]);
            data.Add("beschrijving", collection["beschrijving"]);
            data.Add("prijs", collection["prijs"]);
            data.Add("korting", collection["korting"]);
            data.Add("voorraad", collection["voorraad"]);
            data.Add("afbeelding", collection["afbeelding"]);
            data.Add("thumbnail", collection["thumbnail"]);

            var query = new Query();
            return query.Execute("product", data);

        }

        public void Update()
        {

        }

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
