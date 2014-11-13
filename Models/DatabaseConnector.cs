using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace IntoSport.Models
{
    public class DatabaseConnector
    {
        private static DatabaseConnector databaseConnector;
        public MySqlConnection conn;

        private string server = "localhost";
        private string db = "intosport";
        private string username = "admin";
        private string password = "admin";

        public DatabaseConnector()
        {
            conn = new MySqlConnection("Server=" + server + ";Database=" + db + ";Uid=" + username + ";Pwd=" + password + ";");
            databaseConnector = this;
        }

    }
}