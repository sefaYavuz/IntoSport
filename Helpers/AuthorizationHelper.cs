using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IntoSport.Models;

namespace IntoSport.Helpers
{
    public class Authorization : RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { return "Intosport"; }
            set { }
        }

        public override void CreateRole(string roleName)
        {

        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return new string[0];
            //throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] s = new string[1];
            s[0] = "klant";
            User user = User.GetUser(username);
            if (user != null)
            {
                s[0] = user.role.ToString();
            }
            return s;

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}