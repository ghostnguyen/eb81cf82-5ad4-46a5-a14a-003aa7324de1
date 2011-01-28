<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.UserModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <table>
        <tr>
            <th>
                UserName
            </th>
            <th>
                Roles
            </th>
            <th>
            </th>
        </tr>
        <% 
            var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.AccountController>("Edit", r => r.Edit(""), new RouteValueDictionary(new { id = "userName" }), null, false);
        %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: item.Username %>
            </td>
            <td>
                <%: item.RolesList %>
            </td>
            <td>
                <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.Username }) %>--%>
                <%: MvcHtmlString.Create(editTemplate.ToString().Replace("userName", item.Username))%>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%--<%: Html.ActionLinkWithRoles<OAMS.Controllers.AccountController>("Create New", r => r.Register(), null, null, false)%>--%>
    </p>
</asp:Content>
