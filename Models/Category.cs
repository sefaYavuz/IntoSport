using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace IntoSport.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public string naam { get; set; }
        public int id { get; set; }
        public Category parent { get; set; }

        public Category()
        {
            this.naam = "";
            this.id = 0;
        }
        public Category(int id)
        {
            Query q = new Query();
            q.Select("c.*");
            q.From("categorie c");
            q.Where("id='" + id + "'");
            foreach (Dictionary<string, object> cat in q.Execute())
            {
                this.id = (int)cat["id"];
                this.naam = (string)cat["naam"];
                if (!DBNull.Value.Equals(cat["parent"]))
                {
                    this.parent = new Category(Convert.ToInt32(cat["parent"]));
                }
                else
                {
                    this.parent = new Category();
                }

            }
        }
        public bool Update()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", this.naam);
            data.Add("parent", this.parent.id);
            data.Add("id", this.id);
            var query = new Query();
            return (query.Execute("categorie", data) > 0);
        }

        public int Insert()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", this.naam);
            data.Add("parent", this.parent.id);
            var query = new Query();
            return query.Execute("categorie", data);
        }

        public static List<Dictionary<string, object>> getAllCategories(String search = "", int page = 0)
        {
            Query query = new Query();
            query.Select("c.*, p.naam AS parentname, (SELECT COUNT(*) FROM categorie) AS count");
            query.From("categorie c");
            query.Join("LEFT", "categorie p ON c.parent = p.id");
            query.Group("c.id");
            if (search.Length > 0)
            {
                query.Where("CONCAT(c.id, c.naam, p.naam) LIKE '%" + search + "%'");
            }
            else
            {
               // int pagination = page * Convert.ToInt32(Config.getConfig("rows_per_page"));
               // query.Order("id ASC LIMIT " + pagination + ", " + Config.getConfig("rows_per_page"));
            }
            return query.Execute();
        }

        public static List<Category> getCategoryList()
        {
            List<Category> categories = new List<Category>();
            Query query = new Query();
            query.Select("id");
            query.From("categorie");
            query.Where("parent != '0'");
            query.Order("parent");
            foreach (Dictionary<string, object> cat in query.Execute())
            {
                categories.Add(new Category((int)cat["id"]));
            }
            return categories;
        }


        public static List<SelectListItem> getCategoryList(Category category)
        {
            Query query = new Query();
            query.Select("*");
            query.From("categorie");
            query.Where("parent = 0 OR parent IS NULL");
            List<Dictionary<string, object>> parents = query.Execute();

            List<SelectListItem> ls = new List<SelectListItem>();
            ls.Add(new SelectListItem() { Text = "--", Value = "0" });
            foreach (Dictionary<string, object> parent in (List<Dictionary<string, object>>)parents)
            {
                if ((int)category.id != (int)parent["id"])
                {
                    if (category.parent != null && (int)category.parent.id == (int)parent["id"])
                    {
                        ls.Add(new SelectListItem() { Text = (String)parent["naam"], Value = parent["id"].ToString(), Selected = true });
                    }
                    else
                    {
                        ls.Add(new SelectListItem() { Text = (String)parent["naam"], Value = parent["id"].ToString() });
                    }
                }
            }

            return ls;

        }

    }
}