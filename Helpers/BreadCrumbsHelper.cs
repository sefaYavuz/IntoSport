using IntoSport.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Helpers
{
    public class BreadCrumbsHelper : DatabaseConnector
    {
        public List<BreadCrumbs> getBreadCrumbs(Product product)
        {

            List<BreadCrumbs> breadCrumbsList = new List<BreadCrumbs>();

            conn.Open();
            string sql = "SELECT * FROM categorie JOIN product_categorie ON categorie.id = product_categorie.categorie_id WHERE product_categorie.product_id = ?id";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlParameter parameter = new MySqlParameter("?id", MySqlDbType.Int32);
            parameter.Value = product.id;
            command.Parameters.Add(parameter);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                BreadCrumbs breadcrumbs = new BreadCrumbs();
                breadcrumbs.naam = reader.GetString("naam");
                breadcrumbs.id = reader.GetInt32("id");
                breadCrumbsList.Add(breadcrumbs);
                
            }

            return breadCrumbsList;
        }
    }
}
