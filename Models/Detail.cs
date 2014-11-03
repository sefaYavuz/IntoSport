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

        public static List<Dictionary<string, object>> getAllDetails(string search = "")
        {
            Query query = new Query();
            query.Select("detail.naam, detail_waarde.id AS dw_id, detail_waarde.detail_id as dwd_id, detail_waarde.waarde");
            query.From("detail");
            query.Join("INNER", "detail_waarde ON detail.id = detail_waarde.detail_id");
            if(search.Length > 0)
            {
                query.Where("naam LIKE '%" + search + "%'");
            }

            return query.Execute();
        }

    }
}