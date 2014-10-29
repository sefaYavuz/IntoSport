using System;
using IntoSport.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace IntoSport
{
	public class OrderDBController : DatabaseConnector
	{
		public OrderDBController ()
		{

		}

		public void makeOrder(string id, string cart)
		{
			Query q = new Query();
			q.Select("MAX(id)");
			q.From("Bestelling");
			q.Limit ("1");
			int maxOrderPlus = 0;

			foreach (Dictionary<string, object> order in q.Execute()) {

				if ((int)order ["id"] == null) {
					maxOrderPlus = 0;
				} else {
					maxOrderPlus = (int)order ["id"] + 1;
				}

			}

			Query q2 = new Query();
			DateTime saveNow = DateTime.Now;

			Dictionary<string, object> bestelling = new Dictionary<string,object> ();
			bestelling.Add ("user_id", id);
			bestelling.Add ("status", "in_behandeling");
			bestelling.Add ("datum", saveNow.ToString ());
			bestelling.Add ("korting", 0);

			q2.Execute("bestelling", bestelling);


			foreach(string value in CartHelper.getItems(cart)){

				Query q3 = new Query ();

				string[] product = value.Split (',');

				Dictionary<string, object> order_regel = new Dictionary<string,object> ();
				order_regel.Add ("product_id", product [0]);
				order_regel.Add ("bestelling_id", maxOrderPlus.ToString();)
				order_regel.Add("hoeveelheid", product[1]);

				q3.Execute("order_regel", order_regel);


			}




		}
	}
}

