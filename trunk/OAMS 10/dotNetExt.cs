using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Web.Mvc;
using OAMS.Models;
using System.Web.Mvc.Html;
using System.Security.Principal;
using OAMS.Controllers;
using System.Web.Routing;


namespace OAMS
{
    /// <summary>
    /// Summary description for dotNetExt
    /// </summary>
    /// 

    public static class dotNetExt
    {
        #region String

        public static int? ToIntNullable4Zero(this string s)
        {
            if (string.IsNullOrEmpty(s.Trim())) return null;

            int i;

            return int.TryParse(s, out i) && i != 0 ? i : new Nullable<int>();
        }

        public static int? ToIntNullable(this string s)
        {
            if (string.IsNullOrEmpty(s.Trim())) return null;

            int i;

            return int.TryParse(s, out i) ? i : new Nullable<int>();
        }

        public static int ToInt(this string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim())) return 0;

            int i;

            return int.TryParse(s, out i) ? i : 0;
        }

        public static string AddNumber(this string s, int num)
        {
            return (s.ToInt() + num).ToString();
        }

        public static Guid ToGuid(this string s)
        {
            if (string.IsNullOrEmpty(s.Trim())) return Guid.Empty;

            Guid g = Guid.Empty;

            try
            {
                g = new Guid(s);
            }
            catch (Exception)
            {

            }

            return g;
        }

        public static string FromGuidToCodabar(this string s)
        {
            string r = s.ToLower().Replace("-", "");

            return r.Replace('a', '-').Replace('b', '$').Replace('c', ':').Replace('d', '/').Replace('e', '.').Replace('f', '+');
        }

        public static string FromCodabarToGuid(this string s)
        {
            return s.Replace('-', 'a').Replace('$', 'b').Replace(':', 'c').Replace('/', 'd').Replace('.', 'e').Replace('+', 'f');
        }

        public static string ToURLCompatible(this string s)
        {
            //return HttpUtility.UrlPathEncode(s);
            return HttpUtility.UrlEncode(s);

            //'	%27	Foot Sign
            //"	%22	Quote
            //#	%23	Number sign
            //%	%25	Percent sign
            //&	%26	Ampersand
            //+	\%2B	Plus sign ¹
            ///	%2F	Solidus (slash)
            //:	%3A	Colon
            //;	%3B	Semicolon
            //<	%3C	Less than
            //>	%3EGreater than
            //?	%3F

            //return s
            //    .Replace("%", "%25")
            //    .Replace("+", "%2B")
            //    .Replace("&", "%26")
            //    .Replace("=", "%3D")
            //    .Replace("<", "%3C")
            //    .Replace(";", "%3B")
            //    ;
        }



        public static string RemoveDiacritics(this string s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];

                if (c == 'Đ') c = 'D';
                if (c == 'đ') c = 'd';

                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static bool IsValidDOB(this string s)
        {
            CultureInfo MyCultureInfo = new CultureInfo("vi-VN");
            DateTime dt;

            if (DateTime.TryParse(s.Trim(), MyCultureInfo, DateTimeStyles.None, out dt))
            {
                return (dt < new DateTime(1888, 1, 1)) ? false : true;
            }
            else
            {
                return false;
            }
        }

        public static DateTime? ToShortDate(this string s)
        {
            DateTime dt;

            if (DateTime.TryParse(s.Trim(), out dt))
            {
                return (DateTime?)dt;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToDatetimeFromVNFormat(this string s)
        {
            CultureInfo MyCultureInfo = new CultureInfo("vi-VN");
            DateTime dt;

            if (DateTime.TryParse(s.Trim(), MyCultureInfo, DateTimeStyles.None, out dt))
            {
                return (DateTime?)dt;
            }
            else
            {
                return null;
            }
        }

        public static string Replace(this string s, int index, char c)
        {
            string sub = s.Substring(index, 1);

            if (string.IsNullOrEmpty(sub)) return s;

            return s.Substring(0, index) + c.ToString() + s.Substring(index + 1);
        }

        public static string ToUrlPicasaPhotoResize(this string s, string strResize = "s320")
        {
            return s.Insert(s.LastIndexOf('/'), "/" + strResize);
        }

        //public static bool IsValidProductCode(this string s)
        //{
        //    return BarcodeBLL.IsValidProductCode(s);
        //}

        //public static bool IsValidDINCode(this string s)
        //{
        //    return BarcodeBLL.IsValidDINCode(s);
        //}

        //public static bool IsValidProductCodeList(this List<string> s)
        //{
        //    return s.Where(r => BarcodeBLL.IsValidProductCode(r) == false).Count() == 0;
        //}

        //public static bool IsValidDINList(this List<string> s)
        //{
        //    return s.Where(r => BarcodeBLL.IsValidDINCode(r) == false).Count() == 0;
        //}

        #endregion

        public static TreeNode Find(this System.Web.UI.WebControls.TreeNode node, string childValue)
        {
            foreach (TreeNode childNode in node.ChildNodes)
            {
                if (childNode.Value == childValue)
                    return childNode;
            }

            return null;
        }

        public static SiteMapNode Find(this SiteMapNode node, string url)
        {
            SiteMapNode f;
            foreach (SiteMapNode e in node.ChildNodes)
            {
                if (e.Url.ToLower().Trim() == url.ToLower().Trim())
                {
                    f = e;
                }
                else f = e.Find(url);

                if (f != null) return f;
            }
            return null;
        }

        #region DateTime

        //public static string ToStringVN(this DateTime dt)
        //{
        //    return dt.ToString("dd/MM/yyyy");
        //}

        //public static string ToStringVNLong(this DateTime dt)
        //{
        //    return dt.ToString("dd/MM/yyyy HH:mm:ss");
        //}

        public static string ToShortDateString(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToShortDateString() : "";
        }

        //public static string ToStringVN_Hour(this DateTime? dt)
        //{
        //    if (dt.HasValue)
        //    {
        //        return dt.Value.ToString("dd/MM/yyyy HH:mm");
        //    }
        //    return "";
        //}

        public static DateTime AddMonthsAvoidWeekend(this DateTime dt, int months)
        {
            DateTime temp = dt.AddMonths(months);

            if (temp.DayOfWeek == DayOfWeek.Saturday)
                temp = temp.AddDays(2);

            if (temp.DayOfWeek == DayOfWeek.Sunday)
                temp = temp.AddDays(1);

            return temp;
        }

        /// <summary>
        /// Ex: 1970, 1980, 1990.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int Decade(this DateTime dt)
        {
            return dt.Year - dt.Year % 10;
        }

        //public static int DurationInDays(this DateTime dt)
        //{
        //    return (dt - RedBloodSystem.RootTime).Days;
        //}

        public static bool Expired(this DateTime dt)
        {
            return dt <= DateTime.Now.Date;
        }

        public static bool ExpiredInDays(this DateTime dt, int days)
        {
            return dt <= DateTime.Now.Date.AddDays(days);
        }

        public static DateTime? Ceiling(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.Date.AddDays(1).AddSeconds(-1);
            }
            return dt;
        }

        #endregion

        public static void SelectByText(this DropDownList ddl, string text)
        {
            ListItem li = ddl.Items.FindByText(text);
            if (li != null)
                ddl.SelectedValue = li.Value;
        }

        public static object SelectedValue(this BulletedList bl)
        {
            foreach (ListItem item in bl.Items)
            {
                if (item.Selected == true)
                {
                    return item.Value;
                }
            }
            return 0;
        }

        public static string ToStringRemoveZero(this int i)
        {
            return i == 0 ? "" : i.ToString();
        }

        public static string ToStringRemoveZero(this int? i)
        {
            return i.HasValue ? i.Value.ToStringRemoveZero() : "";
        }

        public static string ToStringOrDefault(this int? i, string nodata = "0")
        {
            return i.HasValue ? i.Value.ToString() : nodata;
        }

        public static string ToRating(this int? i, string nodata = "Not rate")
        {
            string str = nodata;

            if (i.HasValue)
            {
                if (i > 100)
                {

                }
                else if (i == 100)
                {
                    str = "Prime";
                }
                else if (i >= 90)
                {
                    str = "Superb";
                }
                else if (i >= 80)
                {
                    str = "Great";
                }
                else if (i >= 70)
                {
                    str = "Good";
                }
                else if (i >= 60)
                {
                    str = "Fair";
                }
                else if (i >= 50)
                {
                    str = "Mediocre";
                }
                else if (i >= 40)
                {
                    str = "Poor";
                }
                else if (i >= 30)
                {
                    str = "Bad";
                }
                else if (i >= 20)
                {
                    str = "Terrible";
                }
                else if (i >= 1)
                {
                    str = "Abysmal";
                }
            }

            return str;
        }

        public static int ToInt(this object o)
        {
            if (o == null) return 0;
            else return o.ToString().ToInt();
        }

        public static int? ToIntNullable(this object o)
        {
            if (o == null) return null;
            else return o.ToString().ToIntNullable();
        }

        public static double? TrimDouble(this double? d)
        {
            string s = d.ToString();
            int dotIndex = s.IndexOf('.');
            if (dotIndex > -1)
            {
                string thapphan = s.Substring(dotIndex + 1);
                thapphan = thapphan.Substring(0, 1);
                string nguyen = s.Substring(0, dotIndex + 1);
                return double.Parse(nguyen + thapphan);
            }
            else
            {
                return d;
            }
        }

        public static bool HasBaseType(this Type type, Type baseType)
        {
            if (type == null || type.BaseType == null)
            {
                return false;
            }
            else
            {
                if (type.BaseType == baseType)
                {
                    return true;
                }
                else
                {
                    return type.BaseType.HasBaseType(baseType);
                }
            }
        }

        //public static void Apply(this CssStyleCollection style, PrintSetting setting)
        //{
        //    if (setting == null) return;

        //    if (string.IsNullOrEmpty(setting.Top))
        //    { style.Remove("top"); }
        //    else
        //    { style.Add("top", setting.Top); }

        //    if (string.IsNullOrEmpty(setting.Left))
        //    { style.Remove("left"); }
        //    else
        //    { style.Add("left", setting.Left); }

        //    if (string.IsNullOrEmpty(setting.Font))
        //    { style.Remove("font"); }
        //    else
        //    { style.Add("font", setting.Font); }

        //    if (string.IsNullOrEmpty(setting.Size))
        //    { style.Remove("font-size"); }
        //    else
        //    { style.Add("font-size", setting.Size); }

        //    if (string.IsNullOrEmpty(setting.Width))
        //    { style.Remove("width"); }
        //    else
        //    { style.Add("width", setting.Width); }

        //    if (string.IsNullOrEmpty(setting.Height))
        //    { style.Remove("height"); }
        //    else
        //    { style.Add("height", setting.Height); }
        //}

        public static void Visibility(this CssStyleCollection style, bool visibility)
        {
            style.Remove("visibility");

            if (visibility)
                style.Add("visibility", "visible");
            else
                style.Add("visibility", "hidden");
        }

        public static void Alert(this Page p, string msg)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "",
                         "alert ('" + msg + "');", true);
        }

        public static IQueryable<SelectListItem> ToSelectListItem(this IQueryable<Geo> l)
        {
            return l.ToList().Select(r => new SelectListItem() { Value = r.ID.ToString(), Text = r.FullName }).AsQueryable();
        }

        public static IQueryable<SelectListItem> ToSelectListItem(this IQueryable<CodeMaster> l)
        {
            return l.Select(r => new SelectListItem() { Value = r.Code, Text = r.Note });
        }

        public static IQueryable<SelectListItem> ToSelectListItem(this IQueryable<Contractor> l)
        {
            return l.ToList().Select(r => new SelectListItem() { Value = r.ID.ToString(), Text = r.Name }).AsQueryable();
        }

        public static IQueryable<SelectListItem> ToSelectListItem(this IQueryable<Client> l)
        {
            return l.ToList().Select(r => new SelectListItem() { Value = r.ID.ToString(), Text = r.Name }).AsQueryable();
        }

        public static MvcHtmlString CodeMasterDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            CodeMasterRepository repo = new CodeMasterRepository();

            return htmlHelper.DropDownListFor(expression, repo.Get(expression.Body).ToSelectListItem(), OAMSSetting.messageL.SelectNone);
        }

        public static MvcHtmlString CodeMasterDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isSelectNone)
        {
            CodeMasterRepository repo = new CodeMasterRepository();

            return htmlHelper.DropDownListFor(expression, repo.Get(expression.Body).ToSelectListItem(), isSelectNone ? OAMSSetting.messageL.SelectNone : OAMSSetting.messageL.SelectAll);
        }

        public static MvcHtmlString DropDownListForGeo1<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            GeoRepository geoRepository = new GeoRepository();

            return htmlHelper.DropDownListFor(expression, geoRepository.GetByParentID().ToSelectListItem(), OAMSSetting.messageL.SelectNone);
        }

        public static MvcHtmlString ActionLinkWithRoles<T>(this HtmlHelper html, string linkText, Expression<Func<T, ActionResult>> action, RouteValueDictionary routeValues = null, IDictionary<string, object> htmlAttributes = null, bool isPost= false) where T : Controller
        {
            MvcHtmlString htmlStr = MvcHtmlString.Create("");

            ReflectedControllerDescriptor controllerDes = new ReflectedControllerDescriptor(typeof(T));
            string controllerName = controllerDes.ControllerName;

            MethodCallExpression methodExp = action.Body as MethodCallExpression;
            if (methodExp != null)
            {
                string actionName = methodExp.Method.Name;
                ControllerActionRepository controllerActionRepository = new ControllerActionRepository();
                ControllerAction controllerAction = controllerActionRepository.GetAction(controllerName, actionName, isPost);
                if (controllerAction != null)
                {
                    MVCAuthorizationRepository mvcAuthorizationRepository = new MVCAuthorizationRepository();
                    List<string> roles = mvcAuthorizationRepository.GetRolesByControllerAction(controllerAction);

                    CustomAuthorize customAuthorize = new CustomAuthorize() { AuthorizedRoles = roles.ToArray() };
                    if (customAuthorize.Authorize(html.ViewContext.HttpContext))
                    {
                        if (isPost && htmlAttributes == null)
                        {
                            htmlStr = MvcHtmlString.Create("<input type='submit' value='" + linkText + "' />");
                        }
                        else
                        {
                            htmlStr = html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
                        }
                    }
                }
            }
            return htmlStr;
        }

        public static MvcHtmlString ActionLinkWithRoles_Old<T>(this HtmlHelper html, string linkText, Expression<Func<T, ActionResult>> action) where T : Controller
        {
            ControllerActionRepository actionAuthorizationRepo = new ControllerActionRepository();
            actionAuthorizationRepo.UpdateActionList();

            MvcHtmlString htmlStr = null;

            ReflectedControllerDescriptor controllerDes = new ReflectedControllerDescriptor(typeof(T));
            string controllerName = controllerDes.ControllerName;

            MethodCallExpression methodExp = action.Body as MethodCallExpression;
            if (methodExp != null)
            {
                string actionName = methodExp.Method.Name;
                //List<ActionDescriptor> actionDescriptorList = controllerDes.GetCanonicalActions().Where(r => r.ActionName == actionName).ToList();
                //ActionDescriptor actionDes = actionDescriptorList.FirstOrDefault();

                ControllerActionRepository controllerActionRepository = new ControllerActionRepository();
                ControllerAction controllerAction = controllerActionRepository.GetActionWithVerbHttpGet(controllerName, actionName);
                if (controllerAction != null)
                {
                    MVCAuthorizationRepository mvcAuthorizationRepository = new MVCAuthorizationRepository();
                    List<string> roles = mvcAuthorizationRepository.GetRolesByControllerAction(controllerAction);

                    CustomAuthorize customAuthorize = new CustomAuthorize() { AuthorizedRoles = roles.ToArray() };
                    if (customAuthorize.Authorize(html.ViewContext.HttpContext))
                    {
                        htmlStr = html.ActionLink(linkText, actionName, controllerName);
                    }
                }

                //    if (actionDes != null)
                //    {
                //        List<CustomAuthorize> customAuthorizeList = actionDes.GetFilters().AuthorizationFilters.Where(r => r is CustomAuthorize).Select(r => r as CustomAuthorize).ToList();

                //        if (customAuthorizeList.Count > 0)
                //        {
                //            if (customAuthorizeList.Select(r => r.Authorize(html.ViewContext.HttpContext)).Any(r => r))
                //            {
                //                htmlStr = html.ActionLink(linkText, actionName, controllerName);
                //            }
                //        }
                //        else
                //        {
                //            htmlStr = html.ActionLink(linkText, actionName, controllerName);
                //        }

                //        //List<CustomAuthorize> ofController = controllerDes.GetCustomAttributes(typeof(CustomAuthorize), true).Select(r => r as CustomAuthorize).ToList();
                //        //List<CustomAuthorize> ofAction = actionDes.GetCustomAttributes(typeof(CustomAuthorize), true).Select(r => r as CustomAuthorize).ToList();

                //        //List<CustomAuthorize> ofAll = new List<CustomAuthorize>();
                //        //ofAll.AddRange(ofController);
                //        //ofAll.AddRange(ofAction);

                //        //string[] roles = ofAll.SelectMany(r => r.AuthorizedRoles).Distinct().ToArray();
                //        //string[] users = ofAll.SelectMany(r => r.AuthorizedUsers).Distinct().ToArray();

                //        //if (HttpContext.Current.User.HasAnyRole(roles)
                //        //    || users.Contains(OAMSSetting.Username))
                //        //{
                //        //    htmlStr = html.ActionLink(linkText, actionName, controllerName);
                //        //}
                //    }
            }

            return htmlStr;
        }

        public static MvcHtmlString ActionLinkWithRoles(this HtmlHelper html, string linkText, string actionName, string controllerName, params string[] roles)
        {
            //typeof(SiteController).ToString();

            //(new SiteController).Index();

            //ReflectedControllerDescriptor cd = new ReflectedControllerDescriptor();

            //Reflec
            if (HttpContext.Current.User.HasAnyRole(roles))
                return html.ActionLink(linkText, actionName, controllerName);

            return null;
        }

        public static string[] Add(this string[] array, string item)
        {
            List<string> l = array.ToList();
            l.Add(item);
            return l.ToArray();
        }

        public static string GetHttpVerb(this ActionDescriptor actionDesctiptor)
        {
            string httpPost = actionDesctiptor.GetCustomAttributes(typeof(HttpPostAttribute), false).FirstOrDefault() == null ? string.Empty : typeof(HttpPostAttribute).Name;
            string httpGet = actionDesctiptor.GetCustomAttributes(typeof(HttpGetAttribute), false).FirstOrDefault() == null ? string.Empty : typeof(HttpGetAttribute).Name;

            string httpVerb = string.Join(", ", new string[] { httpPost, httpGet }.Where(r => !string.IsNullOrEmpty(r)));
            return httpVerb;
        }

        public static MvcHtmlString DropDownListForSiteMonitoringOrder<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SiteMonitoring siteMonitoring)
        {
            SiteMonitoringRepository repo = new SiteMonitoringRepository();

            return htmlHelper.DropDownListFor(expression, repo.FilterOrder(siteMonitoring), OAMSSetting.messageL.SelectNone);
        }

        public static MvcHtmlString DropDownListForCreateSiteMonitoringOrder<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int contractDetailID)
        {
            SiteMonitoringRepository repo = new SiteMonitoringRepository();

            return htmlHelper.DropDownListFor(expression, repo.FilterOrderForCreate(contractDetailID), OAMSSetting.messageL.SelectNone);
        }

        public static bool Match(this List<string> lst, string str)
        {
            foreach (string item in lst)
            {
                if (item == "")
                {
                    if (str.ToLower() == "blank")
                    {
                        return true;
                    }
                }
                else
                {
                    if (str.ToLower().Contains(item.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsIn(this ITimeRange timerange, DateTime? dFrom, DateTime? dTo)
        {
            return timerange.IsValid()
                && dFrom.HasValue
                && dTo.HasValue
                && dFrom <= dTo
                && timerange.FromDate >= dFrom
                && timerange.ToDate <= dTo;
        }

        public static string ToCustomeString(this bool? value, string strTrue = "Yes", string strFalse = "No")
        {
            return value.HasValue ? (value.Value ? strTrue : strFalse) : "";
        }
    }

    public static class IPrincipalExtend
    {
        public static bool HasAnyRole(this IPrincipal user, params string[] roles)
        {
            //if (!roles.Contains(ProjectRoles.Admin))
            //{
            //    List<string> rL = roles.ToList();
            //    rL.Add(ProjectRoles.Admin);
            //    roles = rL.ToArray();
            //}

            return roles.Any(user.IsInRole);
        }
    }
}
