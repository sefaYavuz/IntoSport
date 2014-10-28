using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Models
{
    public class Product 
    {
        public int id{ get; set; }
        public int categorie_id { get; set; }
        public int detail_id { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public string naam { get; set; }
        public string beschrijving { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public double prijs { get; set; }
        public int korting { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]      
        public int voorraad { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public string afbeelding { get; set; }
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public string thumbnail { get; set; }

        public Product(int productID)
        {
            Query query = new Query();
            query.Select("*");
            query.From("product");
            query.Join("INNER", "product_categorie AS pc ON product.id = pc.product_id");
            query.Join("LEFT OUTER", "detail_product AS dp ON product.id = dp.product_id");
            query.Where("id = " + productID);

            foreach(Dictionary<string, object> product in query.Execute())
            {
                this.id = (int)product["id"];
                this.categorie_id = (int)product["categorie_id"];
                this.naam = (string)product["naam"];
                this.beschrijving = (string)product["beschrijving"];
                this.prijs = (double)product["prijs"];
                this.korting = (int)product["korting"];
                this.voorraad = (int)product["voorraad"];
                this.afbeelding = (string)product["afbeelding"];
                this.thumbnail = (string)product["thumbnail"];
            }
        }
        /*
        public static Product GetProduct(int productID)
        {
            var p = new Product();
            var query = new Query();
            Object temp;

            query.Select("*");
            query.From("product");
            query.Join("INNER", "product_categorie AS pc ON product.id = pc.product_id");
            query.Join("LEFT OUTER", "detail_product AS dp ON product.id = dp.product_id");
            query.Where("id = " + productID);
            string queryString = query.getQuery();
            var product = query.Execute();

            if(product.Count > 0)
            {
                product[0].TryGetValue("id", out temp);
                p.id = (int)temp;

                temp = null;
                product[0].TryGetValue("categorie_id", out temp);
                p.categorie_id = (int)temp;

                temp = null;
                product[0].TryGetValue("detail_id", out temp);
                p.detail_id = (int)temp;

                temp = null;
                product[0].TryGetValue("naam", out temp);
                p.naam = (string)temp;

                temp = null;
                product[0].TryGetValue("beschrijving", out temp);
                p.beschrijving = (string)temp;

                temp = null;
                product[0].TryGetValue("prijs", out temp);
                p.prijs = (double)temp;

                temp = null;
                product[0].TryGetValue("korting", out temp);
                p.korting = (int)temp;

                temp = null;
                product[0].TryGetValue("voorraad", out temp);
                p.voorraad = (int)temp;

                temp = null;
                product[0].TryGetValue("afbeelding", out temp);
                p.afbeelding = (string)temp;

                temp = null;
                product[0].TryGetValue("thumbnail", out temp);
                p.thumbnail = (string)temp;
            }


            return p;
        }*/

        public static int Insert(FormCollection collection)
        {
            Dictionary<string, object> data = new Dictionary<string,object>();
            data.Add("naam", collection["naam"]);
            data.Add("beschrijving", collection["beschrijving"]);
            data.Add("prijs", collection["prijs"]);
            data.Add("korting", collection["korting"]);
            data.Add("voorraad", collection["voorraad"]);
            //data.Add("afbeelding", collection["afbeelding"]);
            //data.Add("thumbnail", collection["thumbnail"]);

            var query = new Query();
            return query.Execute("product", data);
        }

        public static int InsertCategorie(FormCollection collection)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("categorie_id", collection["categories"]);
            data.Add("product_id", GetLastProductID());

            var query = new Query();
            return query.Execute("product_categorie", data);
        }

        public static int InsertDetail(FormCollection collection)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("categorie_id", collection["details"]);
            data.Add("product_id", GetLastProductID());

            var query = new Query();
            return query.Execute("detail_product", data);
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

        public static int GetLastProductID()
        {
            int productID = 0;

            Query query = new Query();
            query.Select("MAX(id) AS id");
            query.From("product");
            query.Limit("1");

            foreach (Dictionary<string, object> product in query.Execute())
            {
                productID = (int)product["id"];
            }

            return productID;
        }

    }
}
