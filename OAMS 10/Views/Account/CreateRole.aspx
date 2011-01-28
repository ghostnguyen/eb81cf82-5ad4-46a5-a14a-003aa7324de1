<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateRoles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        CreateRoles</h2>
    <% using (Html.BeginForm())
       {%>
    <input type="text" id="roleName" name="roleName" />
    <input type="submit" value="Save" />
    <%} %>
</asp:Content>
