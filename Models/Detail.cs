using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Models
{
    public class Detail
    {
        public int id { get; set; }
        public string naam { get; set; }
        public List<DetailWaarde> waardes { get; set; }
        
        public void Save()
        {
            DatabaseConnector databaseconnector = new DatabaseConnector();
            databaseconnector.conn.Open();
         
            string sql = " insert into  detail(naam) OUTPUT INSERTED.ID values(?naam)";
            MySqlCommand command = new MySqlCommand(sql,databaseconnector.conn);
            MySqlParameter naam = new MySqlParameter("?naam", MySqlDbType.String);
         
            naam.Value = this.naam;
            command.Parameters.Add(naam);
            this.id = (Int32)command.ExecuteScalar();
            
            foreach(DetailWaarde waarde in waardes)
            {
                string detail_waarde_sql = "insert into detail_waarde(detail_id,waarde) INSERTED.ID values(?detail_id,?waarde)";
                MySqlCommand detail_command = new MySqlCommand(detail_waarde_sql, databaseconnector.conn);
                MySqlParameter detail_id = new MySqlParameter("?detail_id", MySqlDbType.Int32);
                MySqlParameter detail_waarde = new MySqlParameter("?waarde", MySqlDbType.String);
                detail_id.Value = this.id;
                detail_waarde.Value = waarde.waarde;

                detail_command.Parameters.Add(detail_id);
                detail_command.Parameters.Add(detail_waarde);
                int detail_waarde_id = (Int32)detail_command.ExecuteScalar();

            }
        }
        public static List<Dictionary<string, object>> getAllDetails(string search = "")
        {
            Query query = new Query();
            query.Select("detail.naam, detail_waarde.id AS dw_id, detail_waarde.detail_id as dwd_id, detail_waarde.waarde");
            query.From("detail");
            query.Join("INNER", "detail_waarde ON detail.id = detail_waarde.detail_id");
            if(search.Length > 0)
            {
                query.Where("naam LIKE '%" + search + "%'");
            }

            return query.Execute();
        }

    }
}