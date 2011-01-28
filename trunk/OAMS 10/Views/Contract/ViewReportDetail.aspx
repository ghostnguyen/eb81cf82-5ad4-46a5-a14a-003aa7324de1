<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteRpt.Master" Inherits="System.Web.Mvc.ViewPage<List<OAMS.Models.SiteMonitoring>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% foreach (var item in Model)
       {%>
    <div class="pageBreak">
        <% Html.RenderPartial("~/Views/SiteMonitoring/DisplayTemplates/ViewDetail.ascx", item); %>
    </div>
    <%} %>
</asp:Content>
