using IntoSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace IntoSport.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Detail()
        {
            return View();
        }

        /*
        public List<Omzet> MeestVerkochteProducten()
        {
            conn.Open();
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
<<<<<<< HEAD

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

=======
        */
>>>>>>> 3eac7bf53bfe9a1148412fff01e6d4ecab66bb44
    }
}
