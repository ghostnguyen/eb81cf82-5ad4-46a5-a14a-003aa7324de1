<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractDetailTimeline>" %>
<%: Html.ValidationSummary(true) %>
<fieldset>
    <legend>Fields</legend>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.Order) %>
    <%: Html.HiddenFor(model => model.ContractDetailID)%>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Order) %>:
        <%: Html.DisplayFor(model => model.Order) %>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.FromDate) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.FromDate) %>
        <%: Html.ValidationMessageFor(model => model.FromDate) %>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.ToDate) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.ToDate) %>
        <%: Html.ValidationMessageFor(model => model.ToDate) %>
    </div>
    <p>
        <%--<%: Html.ActionLink("Save", "Edit", "ContractDetailTimeline", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divContractDetailTimeline_" + Model.ID.ToString(), Url.Content("~/ContractDetailTimeLine/Edit")) })%>
        |
        <%: Html.ActionLink("Cancel", "View", "ContractDetailTimeline", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divContractDetailTimeline_" + Model.ID.ToString(), Url.Content("~/ContractDetailTimeLine/View")) })%>--%>
        <%: MvcHtmlString.Create(Session["ContractDetailTimelineSaveTemplate"].ToString().Replace("contractDetailTimelineID", Model.ID.ToString()))%>
        |
        <%: MvcHtmlString.Create(Session["ContractDetailTimelineCancelTemplate"].ToString().Replace("contractDetailTimelineID", Model.ID.ToString()))%>
    </p>
</fieldset>
