using IntoSport.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Helpers
{
    public class OmzetHelper : DatabaseConnector
    {


        public List<Omzet> MeestVerkochteProducten()
        {
            conn.Open();
            List<Omzet> omzets = new List<Omzet>();
            //  string sql = " SELECT naam, count(*) as aantal_verkochte, (prijs * count(*)) as omzet FROM product Group By product.id";
            string sql = "SELECT product.naam, SUM(order_regel.hoeveelheid) as aantal_verkochte,(SUM(product.prijs) * SUM(order_regel.hoeveelheid) )as omzet FROM order_regel JOIN product ON order_regel.product_id = product.id GROUP BY product.id ORDER BY omzet DESC";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() != false)
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


        public List<Omzet> MinstVerkochteProducten()
        {
            conn.Open();
            List<Omzet> omzets = new List<Omzet>();
            //  string sql = " SELECT naam, count(*) as aantal_verkochte, (prijs * count(*)) as omzet FROM product Group By product.id";
            string sql = "SELECT product.naam, SUM(order_regel.hoeveelheid) as aantal_verkochte,(SUM(product.prijs) * SUM(order_regel.hoeveelheid) )as omzet FROM order_regel JOIN product ON order_regel.product_id = product.id GROUP BY product.id ORDER BY omzet ASC";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() != false)
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