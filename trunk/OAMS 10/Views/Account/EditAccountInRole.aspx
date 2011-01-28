<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Acouunt In Role
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%: Html.DisplayFor(model => model) %>
        <input type="text" readonly="readonly" value='<%: Model %>' name="roleName" />
        <div class="editor-label">
            User List
        </div>
        <%
            foreach (MembershipUser item in Membership.GetAllUsers())
            {
                string username = item.UserName;
                string strChecked = Roles.IsUserInRole(username, Model) ? "checked" : string.Empty;
        %>
        <div>
            <% if (Roles.IsUserInRole(item.UserName, Model))
               {
            %>
            <input type="checkbox" name="UserList" value="<%= username %>" checked="checked"
                id='chk_<%= username %>' />
            <% }
               else
               {%>
            <input type="checkbox" name="UserList" value="<%= username %>" id='Checkbox1' />
            <%} %>
            <label for='chk_<%= username %>'>
                <%: username %>
            </label>
        </div>
        <%
            }
        %>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "GetAllRoles") %>
    </div>
</asp:Content>
