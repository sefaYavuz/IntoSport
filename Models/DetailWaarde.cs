using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Models
{
    public class DetailWaarde
    {
        public int id { get; set; }
        public string waarde { get; set; }

        public void Save(int did)
        {
            Query query = new Query();
            query.Select("id");
            query.From("detail_waarde");
            query.Where("waarde = '" + this.waarde + "'");
            query.Where("detail_id = '" + did + "'");

            if(query.Execute().Count == 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (this.id != 0)
                {
                    dic.Add("id", this.id);
                }
                dic.Add("detail_id", did);
                dic.Add("waarde", this.waarde);
                Query q = new Query();
                q.Execute("detail_waarde", dic);
            }
        }
    }
}