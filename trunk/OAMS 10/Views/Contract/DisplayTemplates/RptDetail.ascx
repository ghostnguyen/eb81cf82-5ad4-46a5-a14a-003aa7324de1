<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<OAMS.Models.SiteMonitoring>>" %>
<fieldset>
    <% foreach (var item in Model)
       {%>
    <% Html.RenderPartial("~/Views/SiteMonitoring/ViewDetail.ascx", item); %>
    <%} %>
</fieldset>
