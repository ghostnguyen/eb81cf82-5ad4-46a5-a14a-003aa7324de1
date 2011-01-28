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
    <p>
        <%--<%: Html.ActionLink("Edit", "Edit", "ContractDetailTimeline", new { href = string.Format("javascript:AjaxEdit({0},'{1}','{2}');", Model.ID, "divContractDetailTimeline_" + Model.ID.ToString(), Url.Content("~/ContractDetailTimeline/Edit")) })%>--%>
        <%: MvcHtmlString.Create(Session["ContractDetailTimelineEditTemplate"].ToString().Replace("contractDetailTimelineID", Model.ID.ToString()))%>
    </p>
</div>
