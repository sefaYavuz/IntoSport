using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace IntoSport.Models
{
    public class Query : DatabaseConnector
    {
        private string select;
        private string delete;
        private string from;
        private List<string> where;
        private Hashtable join;
        private List<string> group;
        private List<string> order;

        public Query()
        {
            this.where = new List<string>();
            this.join = new Hashtable();
            this.group = new List<string>();
            this.order = new List<string>();
        }

        public void Select(string s)
        {
            this.select = s;
        }

        public void Delete(string s)
        {
            this.delete = s;
        }

        public void From(string s)
        {
            this.from = s;
        }

        public void Where(string s)
        {
            this.where.Add(s);
        }

        public void Join(string dir, string s)
        {
            this.join.Add(dir, s);
        }

        public void Group(string s)
        {
            this.group.Add(s);
        }

        public void Order(string s)
        {
            this.order.Add(s);
        }

        public string getQuery()
        {
            string queryString = null;

            // SELECT & DELETE
            if (select != null)
            {
                queryString += "SELECT " + @select + " FROM " + @from + " ";
            }
            else if (delete != null)
            {
                queryString += "DELETE FROM " + @delete + " ";
            }

            // JOIN
            if (join.Count > 0)
            {
                foreach (DictionaryEntry o in join)
                {
                    queryString += @o.Key + " JOIN " + @o.Value + " ";
                }
            }

            // WHERE
            if (where.Count > 0)
            {
                foreach (string s in where)
                {
                    if (!queryString.Contains("WHERE"))
                    {
                        queryString += "WHERE " + @s + " ";
                    }
                    else
                    {
                        queryString += "AND " + @s + " ";
                    }
                }
            }
            // GROUP
            if (group.Count > 0)
            {
                foreach (string s in group)
                {
                    if (!queryString.Contains("GROUP BY"))
                    {
                        queryString += "GROUP BY " + @s + " ";
                    }
                    else
                    {
                        queryString += ", " + @s + " ";
                    }
                }
            }

            // ORDER
            if (order.Count > 0)
            {
                foreach (string s in order)
                {
                    if (!queryString.Contains("ORDER BY"))
                    {
                        queryString += "ORDER BY " + @s + " ";
                    }
                    else
                    {
                        queryString += ", " + @s + " ";
                    }
                }
            }

            return queryString;
        }

        public List<Dictionary<string, object>> Execute()
        {
            var result = new List<Dictionary<string, object>>();
            MySqlCommand cmd;
            string queryString = this.getQuery();
            try
            {
                conn.Open();
                cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dictionary.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    result.Add(dictionary);
                }
            }
            catch (Exception ex)
            {
                queryString = this.getQuery();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }



        public int Execute(string table, Dictionary<string, Object> data, List<string> extras = null)
        {
            int rows = 0;

            string query = "";
            try
            {
                conn.Open();
                bool isUpdate = data.ContainsKey("id");
                query += "INSERT INTO `" + @table;
                if (isUpdate)
                {
                    query = "UPDATE `" + @table;
                }
                query += "` SET ";


                foreach (var pair in data)
                {
                    if (pair.Key != "id")
                    {
                        query += " " + pair.Key + "=@" + pair.Key + ",";

                    }
                }
                query = query.Substring(0, query.Length - 1);

                query = isUpdate ? query += " WHERE id=@id" : query;

                if (extras != null)
                {
                    if (!query.Contains("WHERE"))
                    {
                        query += " WHERE ";
                    }
                    foreach (string s in extras)
                    {
                        query += s;
                    }
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                int id = 0;
                foreach (var pair in data)
                {
                    if (id == 0 && pair.Key == "id")
                    {
                        id = int.Parse(pair.Value.ToString());
                    }
                    MySqlParameter param = new MySqlParameter("@" + pair.Key, GetType(pair.Value.GetType()));
                    param.Value = pair.Value;
                    cmd.Parameters.Add(param);
                }

                cmd.ExecuteNonQuery();
                rows = (int)cmd.LastInsertedId;
                rows = rows == 0 ? id : rows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }


        private MySqlDbType GetType(System.Type type)
        {

            if (type.FullName.Equals("System.String"))
            {
                return MySqlDbType.VarChar;
            }
            if (type.FullName.Equals("System.Double"))
            {
                return MySqlDbType.Decimal;
            }
            if (type.FullName.Equals("System.DateTime"))
            {
                return MySqlDbType.DateTime;
            }
            return MySqlDbType.Int32;
        }
    }
}