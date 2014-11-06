using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using IntoSport.Models;
using System.Web.Mvc;
using MySql.Data.MySqlClient;


namespace IntoSport.Helpers 
{
    public class BestellingHelper : DatabaseConnector
    {
        public List<Bestelling> getMyBestellingen(User user)
        {
            List<Bestelling> list = new List<Bestelling>();
            conn.Open();
            MySqlCommand command = new MySqlCommand("select datum,count(product.id) as aantal,status from bestelling join order_regel on order_regel.bestelling_id = bestelling.id join product on order_regel.product_id = product.id where user_id = ?id group by bestelling.id;",conn);
            MySqlParameter param = new MySqlParameter("?id", MySqlDbType.Int16);
            param.Value = user.id;
            command.Parameters.Add(param);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                DateTime dt = Convert.ToDateTime(reader.GetString("datum"));
                Bestelling  bestelling = new Bestelling();
                bestelling.datum = dt;
                bestelling.Aantal_producten =reader.GetInt16("aantal");
                bestelling.status = reader.GetString("status");
                list.Add(bestelling);
            }

            return list;
        }
    }
}