using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntoSport.Models;
using MySql.Data.MySqlClient;

namespace IntoSport.Helpers
{
    public class ProductHelper : DatabaseConnector
    {
        public List<Product> getProduct(int Categorie)
        {
            conn.Open();
            List<Product> productList = new List<Product>();
            string sql = "SELECT * FROM categorie JOIN product_categorie ON categorie.id= product_categorie.categorie_id JOIN product ON product_categorie.product_id = product.id WHERE categorie.id = ?id GROUP BY product.id";
            MySqlCommand command = new MySqlCommand(sql,conn);
            MySqlParameter id = new MySqlParameter("?id", MySqlDbType.Int32);
            command.Parameters.Add(id).Value = Categorie;
            MySqlDataReader reader = command.ExecuteReader();
        
            while(reader.Read())
            {
                productList.Add(new Product(reader.GetInt32("product_id")));
            }  return productList;

        }

        public List<Product> getKortingen()
        {
            List<Product> list = new List<Product>();
            conn.Open();
            string sql = "SELECT id FROM product WHERE korting > 0 ORDER BY id DESC";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                list.Add(new Product(reader.GetInt32("id")));
            }
            return list;
        }

        public List<KeyValuePair<string,string>> getDetails(int product_id)
        {
         
            List<KeyValuePair<string, string>> detailsList = new List<KeyValuePair<string, string>>();
            conn.Open();
            string sql = " select naam,waarde from detail join detail_product on detail.id = detail_product.detail_id join detail_waarde on detail_waarde.detail_id = detail_product.detail_id where product_id =?id";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlParameter id = new MySqlParameter("?id", MySqlDbType.Int32);
            command.Parameters.Add(id).Value = product_id;
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
              
                detailsList.Add( new KeyValuePair<string, string>(reader.GetString("naam"),reader.GetString("waarde")));
            }


            return detailsList;

        }

    }
}