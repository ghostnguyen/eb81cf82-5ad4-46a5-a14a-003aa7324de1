<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Category List</h2>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create", new { parentID = Request.Params["parentID"] })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.CategoryController>("Create New", r => r.Create(null), new RouteValueDictionary(new { parentID = Request.Params["parentID"] }), null, false)%>
        |
        <%--<%: string.IsNullOrEmpty(Request.Params["parentID"]) ? null :  Html.ActionLink("Upper Category", "IndexParent", new { parentID = Request.Params["parentID"] })%>--%>
        <%: string.IsNullOrEmpty(Request.Params["parentID"]) ? null : Html.ActionLinkWithRoles<OAMS.Controllers.CategoryController>("Upper Category", r => r.IndexParent(null), new RouteValueDictionary(new { parentID = Request.Params["parentID"] }), null, false)%>
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
            var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.CategoryController>("Edit", r => r.Edit(new Guid()), new RouteValueDictionary(new { id = "categoryID" }), null, false);
            var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.CategoryController>("Delete", r => r.Delete(new Guid()), new RouteValueDictionary(new { id = "categoryID" }), new Dictionary<string, object>() { { "onclick", "return confirm('Delete?');" } }, false);
            var subTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.CategoryController>("Subs ({0})", r => r.Index(null), new RouteValueDictionary(new { parentID = "categoryParentID" }), null, false);
        %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %>
                |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
                <%= item.Level < 3 ? "| " + Html.ActionLink(string.Format("Subs ({0})", item.Children.Count), "Index", new { parentID = item.ID }) : null %>--%>
                <%: MvcHtmlString.Create(editTemplate.ToString().Replace("categoryID", item.ID.ToString()))%>
                |
                <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("categoryID", item.ID.ToString()))%>
                <%= item.Level < 3 ? "| " + MvcHtmlString.Create(string.Format(subTemplate.ToString().Replace("categoryParentID", item.ID.ToString()), item.Children.Count)) : null%>
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
