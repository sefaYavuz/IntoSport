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
            this.categorie_id = 0;
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

        public int InsertCategorie()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("categorie_id", this.categorie_id);
            data.Add("product_id", GetLastProductID());

            var query = new Query();
            return query.Execute("product_categorie", data);
        }

        public int InsertDetail()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("categorie_id", this.detail_id);
            data.Add("product_id", GetLastProductID());

            var query = new Query();
            return query.Execute("detail_product", data);
        }

        public bool Update()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", this.naam);
            data.Add("beschrijving", this.beschrijving);
            data.Add("prijs", this.prijs);
            data.Add("korting", this.korting);
            data.Add("voorraad", this.voorraad);
            data.Add("afbeelding", this.afbeelding);
            data.Add("thumbnail", this.thumbnail);
            data.Add("id", this.id);

            var query = new Query();
            return query.Execute("product", data) > 0;
        }

        public int UpdateCategorie(int productID)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("categorie_id", this.categorie_id);
            data.Add("product_id", productID);

            var query = new Query();
            return query.Execute("product_categorie", data);
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

    }
}
