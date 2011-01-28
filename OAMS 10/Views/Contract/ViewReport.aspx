<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteRpt.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Rpt01_Model_SiteMonitoring>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.DisplayForModel("ViewReportSumary")%>
</asp:Content>



