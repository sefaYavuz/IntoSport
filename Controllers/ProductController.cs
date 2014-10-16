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
      
        public List<Product> MeestVerkochteProducten()
        {
            List<Product> producten = new List<Product>();
            string sql = " SELECT * FROM product";
            MySqlCommand command = new MySqlCommand();
           
        }

    }
}
