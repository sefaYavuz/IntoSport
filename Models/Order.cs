using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntoSport.Models;
using IntoSport.Helpers;

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
            this.status = Status.betaald;
        }

        public void isVerstuurd()
        {
            this.status =   Status.verstuurd;
        }
        public void isVervallen()
        {
            this.status = Status.vervallen;
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
                this.datum = (string)order["datum"];
                this.korting = (int)order["korting"];

                switch((string)order["status"])
                {
                    case "verstuurd":
                        this.status = Status.verstuurd;
                        break;
                    case "vervallen":
                        this.status = Status.vervallen;
                        break;
                    case "betaald":
                        /*Query q = new Query();
                        q.Select("email");
                        q.From("user");
                        q.Where("id = " + @id);
                        string email = "";

                        foreach (Dictionary<string, object> dic in q.Execute())
                        {
                            email = dic["email"].ToString();
                        }

                        Query q2 = new Query();
                        q2.Select("product_id, hoeveelheid");
                        q2.From("order_regel");
                        q2.Where("bestelling_id =" + @id);
                        List<Product> prodList = new List<Product>();
                        List<int> quantList = new List<int>();

                        foreach (Dictionary<string, object> dic in q2.Execute())
                        {
                            //prodList.Add(dic[")
                        }
                        User u = new User;
                        u.id=user_id;
                        List<Bestelling> bList = new BestellingHelper().getMyBestellingen(u);
                        List<Bestelling> bListJaar =  new List<Bestelling>();
                        int totalYearPrice = 0;

                        foreach(Bestelling bestelling in bList){
                            DateTime dt = Convert.ToDateTime(bestelling.datum);
                            if (dt.CompareTo(DateTime.Now.AddYears(-1)) >= 0){
                                bListJaar.Add(bestelling);
                            }
                        }

                        foreach(Bestelling bestelling in bListJaar){
                            Query q = new Query();
                            q.Select("product_id, hoeveelheid");
                            q.From("order_regel");
                            q.Where("order_id =" @bestelling.)
                        }*/
                        
                        this.status = Status.betaald;
                        break;
                    default:
                        this.status = Status.in_behandeling;
                        break;
                }
            }
        }

        public bool UpdateStatus()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", this.status.ToString());
            data.Add("id", this.id);

            var query = new Query();
            return (query.Execute("bestelling", data) > 0);
        }

        public static List<Dictionary<string, object>> GetAllOrders(string search = "", int whereCustomer = 0)
        {
            Query query = new Query();
            query.Select("bestelling.id, bestelling.datum, bestelling.status, bestelling.id AS ordernr, user.id AS klantnr, SUM(order_regel.hoeveelheid * product.prijs) AS bedrag");
            query.From("order_regel");
            query.Join("INNER", "bestelling ON order_regel.bestelling_id = bestelling.id INNER JOIN user ON bestelling.user_id = user.id INNER JOIN product ON order_regel.product_id = product.id");
            
            if(search.Length > 0)
            {
                query.Where("datum '%" + search + "%'");
            }
            if(whereCustomer > 0)
            {
                query.Where("user.id = " + whereCustomer);
            }

            query.Group("bestelling.id");
            query.Having("bedrag IS NOT NULL");
            query.Order("datum DESC");
            return query.Execute();
        }

        public static bool UpdateStock(int product, int amount)
        {
            int stock = 0;

            Query q = new Query();
            q.Select("voorraad");
            q.From("product");
            q.Where("id = " + product);

            foreach(Dictionary<string, object> products in q.Execute())
            {
                stock = (int)products["voorraad"];
            }

            int newStock = stock - amount;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("id", product);
            data.Add("voorraad", newStock);

            return q.Execute("product", data) > 0;

        }

    }
}