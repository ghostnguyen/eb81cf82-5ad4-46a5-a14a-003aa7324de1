using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAMS.Models
{
    public class MVCAuthorizationRepository : BaseRepository<MVCAuthorizationRepository>
    {
        public void SetRoleAuthorization(string roleName, List<int?> ControllerActionIDList)
        {
            aspnet_Roles role = DB.aspnet_Roles.SingleOrDefault(r => r.RoleName == roleName);

            if (role != null)
            {
                if (ControllerActionIDList == null)
                {
                    ControllerActionIDList = new List<int?>();
                }

                List<MVCAuthorization> deleteList = role.MVCAuthorizations.Where(r => !ControllerActionIDList.Contains(r.ControllerActionID)).ToList();
                
                for (int i = 0; i < deleteList.Count(); i++)
                {
                    DB.DeleteObject(deleteList[i]);                        
                }
                
                DB.SaveChanges();

                IEnumerable<int?> addList = ControllerActionIDList.Except(role.MVCAuthorizations.Select(r => r.ControllerActionID));

                foreach (var item in addList)
                {
                    MVCAuthorization mvcAuthorization = new MVCAuthorization();
                    mvcAuthorization.aspnet_Roles = role;
                    mvcAuthorization.ControllerActionID = item;
                    DB.AddToMVCAuthorizations(mvcAuthorization);
                }
                DB.SaveChanges();
            }
        }

        public List<string> GetRolesByActionDescriptor(ActionDescriptor actionDescriptor)
        {
            List<string> roles = new List<string>();
            
            if (actionDescriptor != null)
            {
                ControllerActionRepository controllerActionRepository = new ControllerActionRepository();

                ControllerAction controllerAction = controllerActionRepository.Get(actionDescriptor);

                roles = GetRolesByControllerAction(controllerAction);    
            }
            
            return roles;
        }

        public List<string> GetRolesByControllerAction(ControllerAction controllerAction)
        {
            List<string> roles = new List<string>();

            if (controllerAction != null)
            {
                roles = controllerAction.MVCAuthorizations.Select(r => r.aspnet_Roles.RoleName).Distinct().ToList();
            }

            return roles;
        }
    }
}