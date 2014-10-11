using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using IntoSport.Models;

namespace IntoSport.DatabaseControllers
{
    public class KlantController : Controller
    {
        //
        // GET: /Klant/

        public MySqlConnection conn;

        public KlantController()
        {
            conn = new MySqlConnection("Server=localhost;Database=intosport;Uid=admin;Pwd=admin;");
        }

        public void AddKlant(RegisterKlantModel RKM)
        {
            try
            {
                conn.Open();

                string addCommand = @" INSERT INTO persoon (email, wachtwoord, voornaam, achternaam) VALUES ('@email', '@wachtwoord', '@voornaam', '@achternaam')";
                MySqlCommand cmd = new MySqlCommand(addCommand, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter voornaam = new MySqlParameter("@voornaam", MySqlDbType.VarChar);
                MySqlParameter achternaam = new MySqlParameter("@achternaam", MySqlDbType.VarChar);
                MySqlParameter wachtwoord = new MySqlParameter("@password", MySqlDbType.VarChar);

                emailParam.Value = RKM.Email;
                voornaam.Value = RKM.Voornaam;
                achternaam.Value = RKM.Achternaam;
                wachtwoord.Value = RKM.Password;

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                conn.Close();
            }

            
            
        }

    }
}
