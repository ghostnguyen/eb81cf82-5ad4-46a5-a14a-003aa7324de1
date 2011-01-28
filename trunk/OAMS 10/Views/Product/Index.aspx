<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        //var oTable;
        $(document).ready(function () {
            //oTable = $('#tblResult').dataTable({ "aaSorting": [[1, "desc"]] });
            var oTable = $('#tblResult').dataTable({ "aaSorting": [[1, "desc"]], "iDisplayLength": 20 });
        });
    </script>
    <h2>
        Index</h2>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table id="tblResult" class="display">
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    ID
                </th>
                <th>
                    Name
                </th>
                <th>
                    Note
                </th>
                <th>
                    CategoryID1
                </th>
                <th>
                    CategoryID2
                </th>
                <th>
                    CategoryID3
                </th>
                <th>
                    ClientID
                </th>
            </tr>
        </thead>
        <tbody>
            <% 
                var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "productID" }), null, false);
                var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Delete", r => r.Delete(0), new RouteValueDictionary(new { id = "productID" }), new Dictionary<string, object>() { { "onclick", "return confirm('Delete?');" } }, false);
                var detailTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Details", r => r.Details(0), new RouteValueDictionary(new { id = "productID" }), null, false);
            %>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>--%>
                    <%: MvcHtmlString.Create(editTemplate.ToString().Replace("productID", item.ID.ToString()))%>
                    |
                    <%: MvcHtmlString.Create(detailTemplate.ToString().Replace("productID", item.ID.ToString()))%>
                    |
                    <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("productID", item.ID.ToString()))%>
                </td>
                <td>
                    <%: item.ID %>
                </td>
                <td>
                    <%: item.Name %>
                </td>
                <td>
                    <%: item.Note %>
                </td>
                <td>
                    <%: item.Category1 == null ? "" : item.Category1.Name %>
                </td>
                <td>
                    <%: item.Category2 == null ? "" : item.Category2.Name %>
                </td>
                <td>
                    <%: item.Category3 == null ? "" : item.Category3.Name %>
                </td>
                <td>
                    <%: item.Client == null ? "" :item.Client.Name %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Create New", r => r.Create(), null, null, false)%>
    </p>
</asp:Content>
