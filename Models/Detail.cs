using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Models
{
    public class Detail
    {
        public int id { get; set; }
        public string naam { get; set; }
        public List<DetailWaarde> waardes { get; set; }

        public void Insert()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("naam", this.naam);

        }
    }
}