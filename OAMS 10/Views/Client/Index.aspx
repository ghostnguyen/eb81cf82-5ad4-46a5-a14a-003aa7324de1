﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Client>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Client List</h2>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Create New", r => r.Create(), null, null, false)%>
    </p>
    <div>
        <table id="tblResult" class="display">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Fullname En
                    </th>
                    <th>
                        Fullname Vi
                    </th>
                    <th>
                        Address
                    </th>
                    <th>
                        Phone
                    </th>
                    <th>
                        Fax
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Total Site
                    </th>
                    <th>
                        Note
                    </th>
                </tr>
            </thead>
            <tbody>
                <% 
                    var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "clientID" }), null, false);
                    var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Delete", r => r.Delete(0), new RouteValueDictionary(new { id = "clientID" }), new Dictionary<string, object>() { { "onclick", "return confirm('Delete?');" } }, false);
                %>
                <% foreach (var item in Model)
                   { %>
                <tr>
                    <td>
                        <%: MvcHtmlString.Create(editTemplate.ToString().Replace("clientID", item.ID.ToString()))%>
                        |
                        <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("clientID", item.ID.ToString()))%>
                    </td>
                    <td>
                        <%: item.Name %>
                    </td>
                    <td>
                        <%: item.Fullname_En %>
                    </td>
                    <td>
                        <%: item.Fullname_Vi %>
                    </td>
                    <td>
                        <%: item.Address %>
                    </td>
                    <td>
                        <%: item.Phone %>
                    </td>
                    <td>
                        <%: item.Fax %>
                    </td>
                    <td>
                        <%: item.Email %>
                    </td>
                    <td>
                        <%: item.Products.SelectMany(r => r.SiteDetailMores).Select(r => r.SiteDetail).Distinct().Count()  %>
                    </td>
                    <td>
                        <%: item.Note %>
                    </td>
                </tr>
                <% } %>
            </tbody>
            <tfoot>
                <tr>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                </tr>
            </tfoot>
        </table>
    </div>
    <br />
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Create New", r => r.Create(), null, null, false)%>
    </p>
    <script type="text/javascript">
        $(document).ready(function () {
            var oTable = $('#tblResult').dataTable({ "aaSorting": [[1, "desc"]], "iDisplayLength": 20 });
        });
    </script>
</asp:Content>
