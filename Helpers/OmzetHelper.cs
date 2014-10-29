using IntoSport.Models;
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
            /**Query query = new Query();
               query.Select(" product.naam, SUM(orderregel.hoeveelheid) as aantal_verkochte,(SUM(product.prijs) * SUM(orderregel.hoeveelheid) )as omzet" );
               query.From("orderregel");
               query.Join("join","orderregel.product_id = product.id");
               query.Group(" ")**/
              
            List<Omzet> omzets = new List<Omzet>();
          //  string sql = " SELECT naam, count(*) as aantal_verkochte, (prijs * count(*)) as omzet FROM product Group By product.id";
            string sql = "SELECT product.naam, SUM(orderregel.hoeveelheid) as aantal_verkochte,(SUM(product.prijs) * SUM(orderregel.hoeveelheid) )as omzet FROM orderregel JOIN product ON orderregel.product_id = product.id GROUP BY product.id ORDER BY omzet DESC";
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


        public List<Omzet> MinstVerkochteProducten()
        {
            conn.Open();
            List<Omzet> omzets = new List<Omzet>();
            //  string sql = " SELECT naam, count(*) as aantal_verkochte, (prijs * count(*)) as omzet FROM product Group By product.id";
            string sql = "SELECT product.naam, SUM(orderregel.hoeveelheid) as aantal_verkochte,(SUM(product.prijs) * SUM(orderregel.hoeveelheid) )as omzet FROM orderregel JOIN product ON orderregel.product_id = product.id GROUP BY product.id ORDER BY omzet ASC";
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