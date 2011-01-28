<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Contract>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <div style="overflow: auto;">
        <table>
            <tr>
                <th>
                </th>
                <th>
                    ID
                </th>
                <th>
                    Number
                </th>
                <th>
                    Type
                </th>
                <th>
                    Contractor
                </th>
                <th>
                    Client
                </th>
                <th>
                    SignedDate
                </th>
                <th>
                    ExpriedDate
                </th>
                <th>
                    InspectionFrequency
                </th>
                <th>
                    ReportOn
                </th>
                <th>
                    Value
                </th>
                <th>
                    PaymentTerm
                </th>
                <th>
                    ContactName1
                </th>
                <th>
                    Phone1
                </th>
                <th>
                    Email1
                </th>
                <th>
                    ContractName2
                </th>
                <th>
                    Phone2
                </th>
                <th>
                    Email2
                </th>
            </tr>
            <% 
                var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "contractID" }), null, false);
                var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Delete", r => r.Delete(0), new RouteValueDictionary(new { id = "contractID" }), new Dictionary<string, object>() { { "onclick", "return confirm('Delete?');" } }, false);
            %>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %>
                    |
                    <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>--%>
                    <%: MvcHtmlString.Create(editTemplate.ToString().Replace("contractID", item.ID.ToString()))%>
                    |
                    <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("contractID", item.ID.ToString()))%>
                </td>
                <td>
                    <%: item.ID %>
                </td>
                <td>
                    <%: item.Number %>
                </td>
                <td>
                    <%: item.ContractType %>
                </td>
                <td>
                    <%: item.ContractorName %>
                </td>
                <td>
                    <%: item.ClientName %>
                </td>
                <td>
                    <%: String.Format("{0:d}", item.SignedDate) %>
                </td>
                <td>
                    <%: String.Format("{0:d}", item.ExpiredDate) %>
                </td>
                <td>
                    <%: item.InspectionFrequency %>
                </td>
                <td>
                    <%: item.ReportOn %>
                </td>
                <td>
                    <%: item.Value %>
                </td>
                <td>
                    <%: item.PaymentTerm %>
                </td>
                <td>
                    <%: item.ContactName1 %>
                </td>
                <td>
                    <%: item.Phone1 %>
                </td>
                <td>
                    <%: item.Email1 %>
                </td>
                <td>
                    <%: item.ContractName2 %>
                </td>
                <td>
                    <%: item.Phone2 %>
                </td>
                <td>
                    <%: item.Email2 %>
                </td>
            </tr>
            <% } %>
        </table>
    </div>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Create New", r => r.Create(), null, null, false)%>
    </p>
</asp:Content>
