using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OAMS.Models
{
    public class AccountRepository : BaseRepository
    {
        public bool Exist(string username, string openId)
        {
            bool r = false;
            MembershipUser user = Membership.GetUser(username);

            if (user != null)
            {
                string _openId = AccountProfile.GetByUsername(username).OpenId;
                //r = _openId == openId;
                r = true;
            }

            return r;
        }

        public void Create(string username, string openId)
        {
            MembershipUser user = Membership.CreateUser(username, "Oams123!@#");
            AccountProfile.GetByUsername(username).OpenId = openId;
        }

        public string Create_ByPassLogin()
        {
            string username = "bypasslogin@gmail.com";
            MembershipUser user = Membership.GetUser(username);

            if (user == null)
            {
                user = Membership.CreateUser(username, "Oams123!@#");
            }

            try
            {
                //Roles.AddUserToRole(username, ProjectRoles.Admin);
            }
            catch (Exception)
            {

            }


            return username;
        }

        public List<UserModel> GetAll()
        {
            RoleRepository roleRepo = new RoleRepository();
            List<UserModel> l = new List<UserModel>();
            foreach (MembershipUser item in Membership.GetAllUsers())
            {
                UserModel user = new UserModel();
                user.Username = item.UserName;
                user.RolesList = roleRepo.GetRolesList(item.UserName);

                l.Add(user);
            }

            return l;
        }

        public RoleAuthenticationModel GetRole(string roleName)
        {
            RoleAuthenticationModel e = new RoleAuthenticationModel();
            aspnet_Roles role = DB.aspnet_Roles.Where(r => r.RoleName == roleName).FirstOrDefault();
            if(role != null)
            {
                List<int?> controllerActionIDList = role.MVCAuthorizations.Select(r => r.ControllerActionID).ToList();
                e.RoleName = role.RoleName;
                e.ControllerActionIDList = controllerActionIDList;
            }
            return e;
        }
    }
}