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

        public bool isAuthorized(string email, string password)
        {
            try
            {
                conn.Open();

                string selectQueryStudent = @"SELECT * FROM `persoon` WHERE `email` = @email AND `wachtwoord` = @password";
                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar);
                emailParam.Value = email;
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

        public string[] getRollen(string email)
        {
            try
            {
                conn.Open();

                string selectQueryStudent = @"select rolnaam 
                                                from rol r, user u, rol_user ru 
                                                where r.rol_id = ru.rol_id and u.user_id = ru.user_id and u.username = @username;";


                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<string> rollen = new List<string>();
                while (dataReader.Read())
                {
                    string rolenaam;
                    string roleNumber = dataReader.GetString("role");

                    if(roleNumber.Equals('0'))
                    {
                        rolenaam = "Beheerder";
                    }
                    else if(roleNumber.Equals('1'))
                    {
                        rolenaam = "Manager";
                    }
                    else
                    {
                        rolenaam = "Allebei";
                    }

                    rollen.Add(rolenaam);
                }

                return rollen.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new string[] { };
            }
            finally
            {
                conn.Close();
            }

        }
    
    }
}