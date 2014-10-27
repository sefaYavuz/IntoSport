using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Models
{
    public class Detail
    {
        public int id { get; set; }
        public string naam { get; set; }
        public List<DetailWaarde> waardes { get; set; }

        public void Insert(FormCollection collection)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", collection["naam"]);

            var query = new Query();
            query.Execute("detail", data);
        }

        public static List<Dictionary<string, object>> getAllDetails(string search = "")
        {
            Query query = new Query();
            query.Select("*");
            query.From("detail");
            
            if(search.Length > 0)
            {
                query.Where("naam LIKE '%" + search + "%'");
            }

            return query.Execute();
        }
    }
}