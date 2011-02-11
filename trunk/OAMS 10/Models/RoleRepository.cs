using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OAMS.Models
{
    public static class ProjectRoles
    {
        public const string Admin = "Admin";
    }

    public class RoleRepository : BaseRepository
    {
        public string GetRolesList(string username)
        {
            return string.Join(", ", Roles.GetRolesForUser(username));
        }

        public void SetRoles(string username, string[] roleList)
        {
            if (roleList == null)
            {
                roleList = new string[] { };
            }

            string[] existRoles = Roles.GetRolesForUser(username);

            string[] removeList = existRoles.Except(roleList).ToArray();
            if (removeList.Count() > 0)
                Roles.RemoveUserFromRoles(username, removeList);

            string[] addList = roleList.Except(existRoles).ToArray();
            if (addList.Count() > 0)
                Roles.AddUserToRoles(username, addList);
        }

        public void SetUsersToRole(string rolename, string[] userList)
        {
            if (userList == null)
            {
                userList = new string[] { };
            }

            string[] existUsers = Roles.GetUsersInRole(rolename);

            string[] removeList = existUsers.Except(userList).ToArray();
            if (removeList.Count() > 0)
                Roles.RemoveUsersFromRole(removeList.ToArray(), rolename);

            string[] addList = userList.Except(existUsers).ToArray();

            if (addList.Count() > 0)
                Roles.AddUsersToRole(addList, rolename);
        }
    }
}