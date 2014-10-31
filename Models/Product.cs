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
        public int detail_id { get; set; }
        [Required(ErrorMessage = "Naam")]
        public string naam { get; set; }
        public string beschrijving { get; set; }
        [Required(ErrorMessage = "Prijs")]
        public double prijs { get; set; }
        public int korting { get; set; }
        [Required(ErrorMessage = "Voorraad")]      
        public int voorraad { get; set; } 
        public string afbeelding { get; set; }
        public string thumbnail { get; set; }

        public Product()
        {
            this.id = 0;
            this.detail_id = 0;
            this.naam = "";
            this.beschrijving = "";
            this.prijs = 0;
            this.korting = 0;
            this.voorraad = 0;
            this.afbeelding = "";
            this.thumbnail = "";
        }

        public Product(int productID)
        {
            Query query = new Query();
            query.Select("*");
            query.From("product");
            query.Join("INNER", "product_categorie AS pc ON product.id = pc.product_id");
            query.Where("product.id = " + productID);

            foreach(Dictionary<string, object> product in query.Execute())
            {
                this.id = (int)product["id"];
                this.naam = (string)product["naam"];
                this.beschrijving = (string)product["beschrijving"];
                this.prijs = (double)product["prijs"];
                this.korting = (int)product["korting"];
                this.voorraad = (int)product["voorraad"];
                this.afbeelding = (string)product["afbeelding"];
                this.thumbnail = (string)product["thumbnail"];
            }
        }

        public int Insert()
        {
            Dictionary<string, object> data = new Dictionary<string,object>();
            data.Add("naam", this.naam);
            data.Add("beschrijving", this.beschrijving);
            data.Add("prijs", this.prijs);
            data.Add("korting", this.korting);
            data.Add("voorraad", this.voorraad);
            data.Add("afbeelding", this.afbeelding);
            data.Add("thumbnail", this.thumbnail);

            var query = new Query();
            return query.Execute("product", data);
        }

        public void InsertCategorie(string[] categories)
        {
            Query query = new Query();
            if (categories.Length > 0)
            {
                Dictionary<string, object> data;

                foreach (string categorie in categories)
                {
                    data = new Dictionary<string, object> { { "product_id", GetLastProductID()}, { "categorie_id", int.Parse(categorie) } };
                    query.Execute("product_categorie", data);
                }
            }
           
        }

        public int Update()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", this.naam);
            data.Add("beschrijving", this.beschrijving);
            data.Add("prijs", this.prijs);
            data.Add("korting", this.korting);
            data.Add("voorraad", this.voorraad);
            if (this.afbeelding.Length > 0)
            {
                data.Add("afbeelding", this.afbeelding);
            }
            if (this.thumbnail.Length > 0)
            {
                data.Add("thumbnail", this.thumbnail);
            }
            data.Add("id", this.id);

            var query = new Query();
            return query.Execute("product", data);
        }

        public void UpdateCategorie(string[] categories)
        {
            Query q = new Query();
            q.Delete("product_categorie");
            q.Where("product_id = " + this.id);
            q.Execute();

            if(categories.Length > 0)
            {
                Dictionary<string, object> data;
                foreach(string categorie in categories)
                {
                     data = new Dictionary<string, object> { { "product_id", this.id }, { "categorie_id", int.Parse(categorie) } };
                    q.Execute("product_categorie", data);
                }
            }
        }

        public static List<Dictionary<string, object>> getAllProducts(string search = "", string limit = "", string joinType = "", string join = "")
        {
            Query query = new Query();
            query.Select("*");
            query.From("product");

            if(search.Length > 0)
            {
                query.Where("naam LIKE '%" + search + "%'");
            }
            if(join.Length > 0)
            {
                query.Join(joinType, join);
            }
            if (limit.Length > 0)
            {
                query.Limit(limit);
            }

            query.Order("id DESC");

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

        public static bool IsInCategorie(int product, int categorie)
        {
            Query query = new Query();
            query.Select("*");
            query.From("product_categorie");
            query.Where("product_id = " + product);
            query.Where("categorie_id = " + categorie);

            return query.Execute().Count > 0;
        }

    }
}
