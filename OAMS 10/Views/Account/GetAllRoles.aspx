<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GetAllRoles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        GetAllRoles</h2>
    <%: Html.ActionLinkWithRoles("Create", "CreateRole", "Account")%>
    <table>
        <thead>
            <tr>
                <td>
                    Role
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               {%>
            <tr>
                <td>
                    <%: item %>
                </td>
                <td>
                    <%: Html.ActionLink("Delete", "DeleteRole", "Account", new { id = item }, new { onclick="javascript:return confirm('Delete?');" })%>
                    |
                    <%: Html.ActionLink("Edit Authentication", "EditRoleAuthentication", "Account", new { roleName = item }, null)%>
                    |
                    <%: Html.ActionLink("Edit Users In Role", "EditAccountInRole", "Account", new { roleName = item }, null)%>
                </td>
            </tr>
            <%} %>
        </tbody>
    </table>
</asp:Content>
