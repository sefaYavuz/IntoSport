﻿using System;
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

            query.Having("bedrag IS NOT NULL");
            query.Order("datum DESC");

            return query.Execute();
        }

    }
}