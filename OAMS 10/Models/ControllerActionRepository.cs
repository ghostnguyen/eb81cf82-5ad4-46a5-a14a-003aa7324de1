using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;

namespace OAMS.Models
{
    public class ControllerActionRepository : BaseRepository
    {
        public void UpdateActionList()
        {
            DB.ControllerActions.ToList();

            List<Type> typeList = Assembly.GetExecutingAssembly().GetExportedTypes().Where(r => r.HasBaseType(typeof(Controller))).ToList();


            //Delete non-existing controllers
            foreach (var item in typeList)
            {
                ReflectedControllerDescriptor controllerDes = new ReflectedControllerDescriptor(item);
                string controllerName = controllerDes.ControllerName;

                ControllerAction controllerOfActionAuthorization = DB.ControllerActions.Where(r => r.Controller == controllerName
                    && string.IsNullOrEmpty(r.Action)
                    && string.IsNullOrEmpty(r.HttpVerb)
                    ).FirstOrDefault();

                if (controllerOfActionAuthorization == null)
                {
                    controllerOfActionAuthorization = new ControllerAction();
                    controllerOfActionAuthorization.Controller = controllerName;
                    DB.AddToControllerActions(controllerOfActionAuthorization);
                    Save();
                }

                List<ActionDescriptor> actionDescriptorList = controllerDes.GetCanonicalActions().ToList();

                foreach (var actionDesc in actionDescriptorList)
                {
                    string parameters = string.Join(", ", actionDesc.GetParameters().Select(r => r.ParameterName));

                    string httpVerb = actionDesc.GetHttpVerb();

                    string actionName = actionDesc.ActionName;

                    ControllerAction actionOfActionAuthorization = DB.ControllerActions.Where(r => r.Controller == controllerName && r.Action == actionName && r.HttpVerb == httpVerb).FirstOrDefault();
                    if (actionOfActionAuthorization == null)
                    {
                        actionOfActionAuthorization = new ControllerAction();
                        actionOfActionAuthorization.Controller = controllerName;
                        actionOfActionAuthorization.Action = actionName;
                        actionOfActionAuthorization.Parameters = parameters;
                        actionOfActionAuthorization.HttpVerb = httpVerb;

                        DB.AddToControllerActions(actionOfActionAuthorization);
                        Save();
                    }
                }
            }
        }

        public List<ControllerAction> GetAll4Authorization()
        {
            return DB.ControllerActions.Where(r => !string.IsNullOrEmpty(r.Action)).ToList().OrderBy(r => r.Desc).ToList();
        }

        public ControllerAction Get(ActionDescriptor actionDescriptor)
        {
            string httpVerb = actionDescriptor.GetHttpVerb();
            return DB.ControllerActions.SingleOrDefault(r => r.Controller == actionDescriptor.ControllerDescriptor.ControllerName
                && r.Action == actionDescriptor.ActionName
                && r.HttpVerb == httpVerb);
        }

        public ControllerAction GetActionWithVerbHttpGet(string controllerName, string actionName)
        {
            string httpGetVerb = typeof(HttpGetAttribute).Name;
            return DB.ControllerActions.Where(r => r.Controller == controllerName && r.Action == actionName
                && (string.IsNullOrEmpty(r.HttpVerb) || r.HttpVerb.Contains(httpGetVerb))).FirstOrDefault();

        }

        public ControllerAction GetAction(string controllerName, string actionName, bool isPost)
        {
            string httpVerb = isPost?typeof(HttpPostAttribute).Name:typeof(HttpGetAttribute).Name;
            //if (isPost)
            //{
            //    return DB.ControllerActions.Where(r => 
            //        r.Controller == controllerName 
            //        && r.Action == actionName
            //        && r.HttpVerb.Contains(httpVerb)
                    
            //        ).FirstOrDefault();
            //}
            //else
            //{
                
            //}

            return DB.ControllerActions.Where(r => r.Controller == controllerName && r.Action == actionName
                    && (string.IsNullOrEmpty(r.HttpVerb) || r.HttpVerb.Contains(httpVerb))).FirstOrDefault();

        }
    }
}