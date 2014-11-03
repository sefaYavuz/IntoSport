using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntoSport.Models;
using MySql.Data.MySqlClient;

namespace IntoSport.Helpers
{
    public class KlantGegevensHelper : DatabaseConnector
    {
        public Boolean SaveKlant(IntoSport.Models.User user)
        {
             conn.Open();
             String sql = "Update user set email=?email,wachtwoord=?wachtwoord,voornaam=?voornaam,achternaam=?achternaam,plaats=?plaats,tel=?telefoon,postcode=?postcode where id = ?id";
             MySqlCommand command = new MySqlCommand(sql, conn);
             MySqlParameter userid = new MySqlParameter("?id", MySqlDbType.Int32);
             userid.Value = user.id;
            
             MySqlParameter voornaam = new MySqlParameter("?voornaam", MySqlDbType.String);
             voornaam.Value = user.voornaam;

             MySqlParameter email = new MySqlParameter("?email", MySqlDbType.String);
             email.Value = user.email;
             
             MySqlParameter achternaam = new MySqlParameter("?achternaam", MySqlDbType.String);
             achternaam.Value = user.achternaam;

             MySqlParameter plaats = new MySqlParameter("?plaats", MySqlDbType.String);
             plaats.Value = user.plaats;

             MySqlParameter telefoon  = new MySqlParameter("?telefoon", MySqlDbType.String);
             telefoon.Value = user.tel;

             MySqlParameter wachtwoord = new MySqlParameter("?wachtwoord", MySqlDbType.String);
             wachtwoord.Value = user.wachtwoord;

             MySqlParameter postcode = new MySqlParameter("?postcode", MySqlDbType.String);
             postcode.Value = user.postcode;

             command.Parameters.Add(postcode);
             command.Parameters.Add(userid);
             command.Parameters.Add(voornaam);
             command.Parameters.Add(email);
             command.Parameters.Add(achternaam);
             command.Parameters.Add(plaats);
             command.Parameters.Add(telefoon);
             command.Parameters.Add(wachtwoord);
             
            command.ExecuteNonQuery();

            return true;
        }
    }
}