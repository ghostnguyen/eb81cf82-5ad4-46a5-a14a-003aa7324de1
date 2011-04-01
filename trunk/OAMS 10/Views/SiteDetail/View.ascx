<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<% if (Request.HttpMethod == "GET")
   { %>
<tr id='<%= "divSiteDetail_" + Model.ID.ToString() %>'>
    <%} %>
    <td>
        <%: Model.Name %>
    </td>
    <td>
        <%: Model.Type %>
    </td>
    <td>
        <%: Model.Format %>
    </td>
    <td>
        <%: Model.Height %>
    </td>
    <td>
        <%: Model.Width %>
    </td>
    <td>
        <%: Model.Size %>
    </td>
    <td>
        <%: MvcHtmlString.Create(Session["SiteDetailEditTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
        |
        <%: MvcHtmlString.Create(Session["SiteDetailDeleteTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
    </td>
    <% if (Request.HttpMethod == "GET")
       { %>
</tr>
<tr id='<%= "divSubSiteDetail_" + Model.ID.ToString() %>'>
    <td colspan="7" style="padding-left: 50px;">
        <% Html.RenderPartial("~/Views/SiteDetail/ManageSiteDetailMore.ascx", Model); %>
        <br />
    </td>
</tr>
<%--<tr id='<%= "divSubSiteDetailPhoto_" + Model.ID.ToString() %>'>
    <td colspan="7" style="padding-left: 50px;">
        <% Html.RenderPartial("~/Views/SiteDetail/ManageSiteDetailPhoto.ascx", Model); %>
        <br />
    </td>
</tr>--%>
<%} %>
