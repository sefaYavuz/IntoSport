using System;
using IntoSport.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using IntoSport.Helpers;
using MySql.Data.MySqlClient;

namespace IntoSport
{
    public class OrderDBController : DatabaseConnector
    {
        public OrderDBController()
        {

        }

        public void makeOrder(string email, string cart)
		{

            Query userQuery = new Query();
            userQuery.Select("id");
            userQuery.From("user");
            userQuery.Where("email = " + "\"" +@email + "\"");
            int id = 0;

            foreach (Dictionary<string, object> user in userQuery.Execute())
            {
                    id = (int)user["id"];    
            }

            Query q = new Query();
            q.Select("MAX(id)");
            q.From("bestelling");
            q.Limit("1");
			int maxOrder = 0;

			Query q2 = new Query();
			DateTime saveNow = DateTime.Now;

			Dictionary<string, object> bestelling = new Dictionary<string,object> ();
			bestelling.Add ("user_id", id);
			bestelling.Add ("status", "in_behandeling");
			bestelling.Add ("datum", saveNow.ToString ());
			bestelling.Add ("korting", 0);
           
			q2.Execute("bestelling", bestelling);

            foreach (Dictionary<string, object> order in q.Execute())
            {
                    maxOrder = (int)order["MAX(id)"];
            }



            string[] itemList = CartHelper.getItems(cart);
            for (int i = 0; i < CartHelper.getItems(cart).Length - 1; i++)
            {
                /*string value = itemList[i];
                var q3 = new Query();

                string[] product = value.Split(',');

                Dictionary<string, object> order_regel = new Dictionary<string, object>();
                order_regel.Add("product_id", Int32.Parse(product[0]));
                order_regel.Add("bestelling_id", maxOrder);
                order_regel.Add("hoeveelheid", Int32.Parse(product[1]));

                q3.Execute("order_regel", order_regel);*/

                string value = itemList[i];

                string[] product = value.Split(',');

                string query = "INSERT INTO `intosport`.`order_regel`(`bestelling_id`,`product_id`,`hoeveelheid`)VALUES(" + maxOrder + "," + product[0] + "," + product[1] + ")";

                Models.Order.UpdateStock(int.Parse(product[0]), int.Parse(product[1]));

                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=" + "127.0.0.1" + ";Database=" + "intosport" + ";Uid=" + "admin" + ";Pwd=" + "admin" + ";");

                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }




		}
    }
}