<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Geo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        National Geography List</h2>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create", new { parentID = Request.Params["parentID"] })%>
        |
        <%: string.IsNullOrEmpty(Request.Params["parentID"]) ? null :  Html.ActionLink("Upper Geos", "IndexParent", new { parentID = Request.Params["parentID"] })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Create New", r => r.Create(null), new RouteValueDictionary(new { parentID = Request.Params["parentID"] }), null, false)%>
        |
        <%: string.IsNullOrEmpty(Request.Params["parentID"]) ? null : Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Upper Geos", r => r.IndexParent(null), new RouteValueDictionary(new { parentID = Request.Params["parentID"] }), null, false)%>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Name
            </th>
            <th>
                Level
            </th>
            <th>
                FullName
            </th>
        </tr>
        <% 
            var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Edit", r => r.Edit(new Guid()), new RouteValueDictionary(new { id = "geoID" }), null, false);
            var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Delete", r => r.Delete(new Guid()), new RouteValueDictionary(new { id = "geoID" }), new Dictionary<string,object> (){{"onclick","return confirm('Delete?');"}}, false);
            var subTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Subs ({0})", r => r.Index(null), new RouteValueDictionary(new { parentID = "geoID" }), null, false);
        %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %>
                |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
                <%= item.Level < 3 ? "| " + Html.ActionLink(string.Format("Subs ({0})", item.Children.Count), "Index", new { parentID = item.ID }) : null %>--%>
                <%: MvcHtmlString.Create(editTemplate.ToString().Replace("geoID", item.ID.ToString()))%>
                |
                <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("geoID", item.ID.ToString()))%>
                <%= item.Level < 3 ? "| " + MvcHtmlString.Create(subTemplate.ToString().Replace("{0}", item.Children.Count.ToString()).Replace("geoID", item.ID.ToString())) : null%>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Level %>
            </td>
            <td>
                <%: item.FullName %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
