using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Models
{
    public class User
    {
        public enum Role
        {
            klant,
            beheerder,
            manager,
            alles
        }


        public virtual int id { get; set; }
        public virtual string voornaam { get; set; }
        public virtual string achternaam { get; set; }
        public virtual string straat { get; set; }
        public virtual string huisnr { get; set; }
        public virtual string postcode { get; set; }
        public virtual string plaats { get; set; }
        public virtual string tel { get; set; }
        public virtual string email { get; set; }
        public virtual string wachtwoord { get; set; }
        public virtual Role role { get; set; }
        public virtual bool goldmember { get; set; }

        public bool IsValid()
        {
            Query query = new Query();
            query.Select("*");
            query.From("user");
            query.Where("email = '" + this.email + "'");
            query.Where("wachtwoord = '" + this.wachtwoord + "'");
            var result = query.Execute();

            if (result.Count > 0)
            {
                this.id = Convert.ToInt32(result[0]["id"]);
                return true;
            }
            return false;
        }

        public static User GetUser(string email = "", int id = 0)
        {
            var obj = new User();
            Query query = new Query();
            query.Select("*");
            query.From("user");
            if (email.Length > 0)
            {
                query.Where("email='" + @email + "'");
            }
            if (id > 0)
            {
                query.Where("id= " + @id);
            }
            string queryString = query.getQuery();
            var user = query.Execute();

            if (user.Count > 0)
            {
                Object temp = null;

                user[0].TryGetValue("id", out temp);
                obj.id = (int)temp;

                temp = null;
                user[0].TryGetValue("voornaam", out temp);
                obj.voornaam = (string)temp;

                temp = null;
                user[0].TryGetValue("achternaam", out temp);
                obj.achternaam = (string)temp;

                temp = null;
                user[0].TryGetValue("straat", out temp);
                obj.straat = (string)temp;

                temp = null;
                user[0].TryGetValue("huisnr", out temp);
                obj.huisnr = (string)temp;

                temp = null;
                user[0].TryGetValue("postcode", out temp);
                obj.postcode = (string)temp;

                temp = null;
                user[0].TryGetValue("plaats", out temp);
                obj.plaats = (string)temp;

                temp = null;
                user[0].TryGetValue("tel", out temp);
                obj.postcode = (string)temp;

                temp = null;
                user[0].TryGetValue("email", out temp);
                obj.email = (string)temp;

                temp = null;
                user[0].TryGetValue("wachtwoord", out temp);
                obj.wachtwoord = (string)temp;

                temp = null;
                user[0].TryGetValue("role", out temp);
                obj.role = (Role)Enum.Parse(typeof(Role), (string)temp);

                temp = null;
                user[0].TryGetValue("goldmember", out temp);
                obj.goldmember = (bool) ((int)temp == 0 ? false : true)  ;

            }

            return obj;
        }

        private bool Insert(Dictionary<string, object> data)
        {
            data.Add("voornaam", this.voornaam);
            data.Add("achternaam", this.achternaam);
            data.Add("straat", this.straat);
            data.Add("huisnr", this.huisnr);
            data.Add("postcode", this.postcode);
            data.Add("plaats", this.plaats);
            data.Add("tel", this.tel);
            data.Add("role", this.role.ToString());
            if (id == 0)
            {
                data.Add("email", this.email);
                data.Add("wachtwoord", this.wachtwoord);
            }


            var query = new Query();
            return (query.Execute("user", data) > 0);
        }

        public bool Insert()
        {
            var data = new Dictionary<string, object>();
            return Insert(data);
        }

    }
}