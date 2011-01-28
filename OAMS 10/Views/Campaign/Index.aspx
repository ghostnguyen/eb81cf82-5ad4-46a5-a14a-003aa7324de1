<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Campaign>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Name
            </th>
            <th>
                ClientID
            </th>
            <th>
                EffectiveDate
            </th>
            <th>
                TermDate
            </th>
            <th>
                Note
            </th>
            <th>
                Cost
            </th>
            <th>
                ProductionCost
            </th>
        </tr>
    <% 
        var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.CampaignController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "campaignID" }), null, false);
    %>
    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>--%>
                <%: MvcHtmlString.Create(editTemplate.ToString().Replace("campaignID", item.ID.ToString()))%>
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.ClientID %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.EffectiveDate) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.TermDate) %>
            </td>
            <td>
                <%: item.Note %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.Cost) %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.ProductionCost) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.CampaignController>("Create New", r => r.Create(), null, null, false)%>
    </p>

</asp:Content>

