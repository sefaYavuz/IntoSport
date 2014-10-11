using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace IntoSport.DatabaseControllers
{
    public class AuthDBController
    {

        private MySqlConnection conn;
        
        public AuthDBController()
        {
            conn = new MySqlConnection("Server=localhost;Database=intosport;Uid=admin;Pwd=admin;");
        }

        public bool isAuthorized(string usernaam, string password)
        {
            try
            {
                conn.Open();

                string selectQueryStudent = @"SELECT * FROM ` users` WHERE `email` = @email AND `password` = @password";

                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar);
                emailParam.Value = usernaam;
                passwordParam.Value = password;
                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                return dataReader.Read();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                conn.Close();
            }
            
        }
    
    }
}