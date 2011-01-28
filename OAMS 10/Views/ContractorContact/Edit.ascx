<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractorContact>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.ContractorID)%>
    <%: Html.TextBoxFor(model => model.Tittle)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Name)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Note)%>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "ContractorContact", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divContractorContact_" + Model.ID.ToString(), Url.Content("~/ContractorContact/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "ContractorContact", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divContractorContact_" + Model.ID.ToString(), Url.Content("~/ContractorContact/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["ContractorContactSaveTemplate"].ToString().Replace("contractorContactID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["ContractorContactCancelTemplate"].ToString().Replace("contractorContactID", Model.ID.ToString()))%>
</td>
