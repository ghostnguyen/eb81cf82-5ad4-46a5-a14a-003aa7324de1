<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractTimeline>" %>
<div id='<%= "divContractTimeline_" + Model.ID.ToString() %>'>
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
        <%--<%: Html.ActionLink("Edit", "Edit", "ContractTimeline", new { href = string.Format("javascript:AjaxEdit({0},'{1}','{2}');", Model.ID, "divContractTimeline_" + Model.ID.ToString(), Url.Content("~/ContractTimeLine/Edit")) })%>--%>
        <%: MvcHtmlString.Create(Session["ContractTimelineEditTemplate"].ToString().Replace("contractTimelineID", Model.ID.ToString()))%>
        <%--|
        <%: Html.ActionLink("Delete", "Delete", "ContractTimeline", new { href = string.Format("javascript:DeleteTimeline({0},'{1}','{2}');", Model.ID, "divContractTimeline_" + Model.ID.ToString(), Url.Content("~/ContractTimeLine/Delete  ")) })%>--%>
    </p>
</div>
