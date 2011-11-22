using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace OAMS.Controllers
{
    public class BaseController<T> : Controller
        where T : BaseController<T>, new()
    {
        static public T I = new T();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns>arr[0] controllerName, arr[1] actionName</returns>
        public static string[] GetControllerNameAndActionName(Expression<Func<T, ActionResult>> action)
        {
            string[] arr = new string[2];

            ReflectedControllerDescriptor controllerDes = new ReflectedControllerDescriptor(typeof(T));
            arr[0] = controllerDes.ControllerName;

            MethodCallExpression methodExp = action.Body as MethodCallExpression;
            if (methodExp != null)
            {
                arr[1] = methodExp.Method.Name;
            }

            return arr;
        }

        public static bool IsAuthorize(string controllerName, string actionName, bool isPost = false)
        {
            var isAuthorize = false;

            ControllerActionRepository controllerActionRepository = new ControllerActionRepository();
            ControllerAction controllerAction = controllerActionRepository.GetAction(controllerName, actionName, isPost);
            if (controllerAction != null)
            {
                MVCAuthorizationRepository mvcAuthorizationRepository = new MVCAuthorizationRepository();
                List<string> roles = mvcAuthorizationRepository.GetRolesByControllerAction(controllerAction);

                CustomAuthorize customAuthorize = new CustomAuthorize() { AuthorizedRoles = roles.ToArray() };

                isAuthorize = customAuthorize.Authorize();
            }

            return isAuthorize;
        }

        public static bool IsAuthorize(Expression<Func<T, ActionResult>> action, bool isPost = false)
        {
            string[] arr = GetControllerNameAndActionName(action);
            return IsAuthorize(arr[0], arr[1], isPost);
        }
    }

    public class BaseController<T, U> : BaseController<U>
        where T : BaseRepository<T>, new()
        where U : BaseController<U>, new()
    {
        private T _repo = new T();
        public T Repo
        {
            get { return _repo; }
        }
    }


    //public class BaseController<T, U> : Controller
    //    where T : BaseRepository<T>, new()
    //    where U : BaseController<T, U>, new()
    //{
    //    private T _repo = new T();
    //    public T Repo
    //    {
    //        get { return _repo; }
    //    }

    //    static public T I = new T();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="action"></param>
    //    /// <returns>arr[0] controllerName, arr[1] actionName</returns>
    //    public static string[] GetControllerNameAndActionName(Expression<Func<U, ActionResult>> action)
    //    {
    //        string[] arr = new string[2];

    //        ReflectedControllerDescriptor controllerDes = new ReflectedControllerDescriptor(typeof(U));
    //        arr[0] = controllerDes.ControllerName;

    //        MethodCallExpression methodExp = action.Body as MethodCallExpression;
    //        if (methodExp != null)
    //        {
    //            arr[1] = methodExp.Method.Name;
    //        }

    //        return arr;
    //    }

    //    public static bool IsAuthorize(string controllerName, string actionName, bool isPost = false)
    //    {
    //        var isAuthorize = false;

    //        ControllerActionRepository controllerActionRepository = new ControllerActionRepository();
    //        ControllerAction controllerAction = controllerActionRepository.GetAction(controllerName, actionName, isPost);
    //        if (controllerAction != null)
    //        {
    //            MVCAuthorizationRepository mvcAuthorizationRepository = new MVCAuthorizationRepository();
    //            List<string> roles = mvcAuthorizationRepository.GetRolesByControllerAction(controllerAction);

    //            CustomAuthorize customAuthorize = new CustomAuthorize() { AuthorizedRoles = roles.ToArray() };

    //            isAuthorize = customAuthorize.Authorize();
    //        }

    //        return isAuthorize;
    //    }

    //    public bool IsAuthorize(Expression<Func<U, ActionResult>> action, bool isPost = false)
    //    {
    //        string[] arr =  GetControllerNameAndActionName(action);
    //        return IsAuthorize(arr[0], arr[1], isPost);
    //    }

    //    public static MvcHtmlString ActionLinkWithRoles(HtmlHelper html, string linkText, Expression<Func<U, ActionResult>> action, RouteValueDictionary routeValues = null, IDictionary<string, object> htmlAttributes = null, bool isPost = false)
    //    {
    //        MvcHtmlString htmlStr = MvcHtmlString.Create("");

    //        string[] arr = GetControllerNameAndActionName(action);
    //        string controllerName = arr[0];
    //        string actionName = arr[1];

    //        if (IsAuthorize(controllerName, actionName, isPost))
    //        {
    //            if (isPost && htmlAttributes == null)
    //            {
    //                htmlStr = MvcHtmlString.Create("<input type='submit' value='" + linkText + "' />");
    //            }
    //            else
    //            {                    
    //                htmlStr = html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
    //            }
    //        }

    //        return htmlStr;
    //    }
    //}
}