using IntoSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace IntoSport.Controllers
{
    public class ProductController : DatabaseConnector
    {
      
        
        public List<Omzet> MeestVerkochteProducten()
        {
            conn.Open();
            List<Omzet> omzets = new List<Omzet>();
            string sql = " SELECT naam, count(*) as aantal_verkochte, (prijs * count(*)) as omzet FROM product Group By product.id";
            MySqlCommand command = new MySqlCommand(sql,conn);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read() != false)
            {

                omzets.Add(
                    new Omzet(
                        reader.GetString("naam"),
                        reader.GetInt32("aantal_verkochte"), 
                        reader.GetDouble("omzet")
                        )
                       );
            }
            return omzets;
        }

    }
}
