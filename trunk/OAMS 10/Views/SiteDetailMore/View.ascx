<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetailMore>" %>
<%@ Import Namespace="OAMS.Controllers" %>
<% if (Request.HttpMethod == "GET")
   { %>
<tr id='<%= "divSiteDetailMore_" + Model.ID.ToString() %>'>
    <%} %>
    <td>
        <%: Model.Product != null ? Model.Product.NewClientName : "_" %>
    </td>
    <td>
        <%: Model.Product == null ? "" : Model.Product.Name %>
    </td>
    <td>
        <%: Model.Product == null ? "" : Model.Product.CategoryFullName%>
    </td>
    <td>
        <%: Html.ActionLinkWithRoles<SiteDetailMoreController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit({0},'divSiteDetailMore_{0}','{1}');", Model.ID.ToString(), Url.Content("~/SiteDetailMore/Edit")) } }, false) %>
        |
        <%: Html.ActionLinkWithRoles<SiteDetailMoreController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete({0},'divSiteDetailMore_{0}','{1}');", Model.ID.ToString(), Url.Content("~/SiteDetailMore/Delete")) } }, true) %>
    </td>
    <% if (Request.HttpMethod == "GET")
       { %>
</tr>
<%} %>
