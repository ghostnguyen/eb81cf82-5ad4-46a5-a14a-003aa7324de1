<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<%@ Import Namespace="OAMS.Controllers" %>
<div id='<%= "divManageSiteDetailMore" + Model.ID.ToString()%>'>
    <table>
        <thead>
            <tr>
                <th>
                    Client
                </th>
                <th>
                    Product
                </th>
                <th>
                    Category
                </th>
                <td>
                    Action
                </td>
            </tr>
        </thead>
        <tbody id='<%= "divManageSiteDetailMoreList" + Model.ID.ToString()%>'>
            <% 
                var editHtml = Html.ActionLinkWithRoles<SiteDetailMoreController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(siteDetailMoreID,'divSiteDetailMore_siteDetailMoreID','{0}');", Url.Content("~/SiteDetailMore/Edit")) } }, false);
                var deleteHtml = Html.ActionLinkWithRoles<SiteDetailMoreController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete(siteDetailMoreID,'divSiteDetailMore_siteDetailMoreID','{0}');", Url.Content("~/SiteDetailMore/Delete")) } }, true);
                var saveHtml = Html.ActionLinkWithRoles<SiteDetailMoreController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divSiteDetailMore_siteDetailMoreID','{0}');", Url.Content("~/SiteDetailMore/Edit")) } }, true);
                var cancelHtml = Html.ActionLinkWithRoles<SiteDetailMoreController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(siteDetailMoreID,'divSiteDetailMore_siteDetailMoreID','{0}');", Url.Content("~/SiteDetailMore/View")) } }, false);
            %>
            <% foreach (var item in Model.SiteDetailMores)
               { %>
            <% Html.RenderPartial("~/Views/SiteDetailMore/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%: Html.ActionLinkWithRoles<SiteDetailMoreController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddSiteDetailMore({0})", Model.ID) } }, false)%>
    </div>
</div>
