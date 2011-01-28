<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractDetailTimeline>" %>
<div id='<%= "divContractDetailTimeline_" + Model.ID.ToString() %>'>
    <div class="display-label">
        Order:
        <%: Model.Order %></div>
    <div class="display-label">
        FromDate:
        <%: String.Format("{0:d}", Model.FromDate) %></div>
    <div class="display-label">
        ToDate:
        <%: String.Format("{0:d}", Model.ToDate) %></div>
</div>
