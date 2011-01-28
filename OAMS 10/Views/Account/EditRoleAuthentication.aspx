<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.RoleAuthenticationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditRoleAuthentication
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        EditRoleAuthentication</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        
        

        <%: Html.DisplayFor(model => model.RoleName) %> <input type="button" value="Clear" onclick="Clear();" />
        <br />


        <%
            List<OAMS.Models.ControllerAction> fullList = (new OAMS.Models.ControllerActionRepository()).GetAll4Authorization();
            foreach (var item in fullList)
            {
                bool isChecked = Model.ControllerActionIDList.Contains(item.ID);
                //string strChecked = isChecked ? "checked" : string.Empty;
        %>
        <div>
            <% if (isChecked)
               {
            %>
            <input type="checkbox" name="ControllerActionIDList" value="<%= item.ID %>" checked="checked"
                id='chk_<%= item.ID %>' />
            <% }
               else
               {%>
            <input type="checkbox" name="ControllerActionIDList" value="<%= item.ID %>" id='chk_<%= item.ID %>' />
            <%} %>
            <label for='chk_<%= item.ID %>'>
                <%: item.Desc %>
            </label>
        </div>
        <%
}
        %>
        <p>
            <!--<input type="submit" value="Save" />-->
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.AccountController>("Save", r => r.EditRoleAuthentication(null), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.AccountController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
    <script type="text/javascript">
        function Clear() { 
        $('input:checkbox').attr('checked','');
        }
    </script>
</asp:Content>
