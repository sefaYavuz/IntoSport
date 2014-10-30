;using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Models
{
    public class Order
    {
        public enum Status 
        {
            in_behandeling,
            betaald,
            verstuurd,
            vervallen
        }

        public int id { get; set; }
        public int user_id { get; set; }
        public Status status { get; set; }
        public string datum { get; set; }
        public int korting { get; set; }
          public void inBehandeling()
          {
           this.status = Status.in_behandeling;
         }
        public void isBetaald()
        {
            this.status = Status.verstuurd;
        }

        public void isVerstuurd()
        {
            this.status =   Status.verstuurd;
        }
        public void isVervallen()
        {
            this.status = Status.verstuurd;
        }
        public Order(int orderID)
        {
            Query query = new Query();
            query.Select("*");
            query.From("bestelling");
            query.Where("id = " + orderID);
            
            foreach(Dictionary<string, object> order in query.Execute())
            {
                this.id = (int)order["id"];
                this.user_id = (int)order["user_id"];
                this.status = (Status)order["status"];
                this.datum = (string)order["datum"];
                this.korting = (int)order["korting"];
            }
        }

        public bool UpdateStatus()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", this.status);
            data.Add("id", this.id);

            var query = new Query();
            return (query.Execute("bestelling", data) > 0);
        }

        public static List<Dictionary<string, object>> GetAllOrders(string search = "")
        {
            Query query = new Query();
            query.Select("*");
            query.From("bestelling");
            
            if(search.Length > 0)
            {
                query.Where("datum '%" + search + "%'");
            }

            query.Order("datum DESC");

            return query.Execute();
        }

    }
}