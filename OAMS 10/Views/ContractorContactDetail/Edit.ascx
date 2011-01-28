<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractorContactDetail>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.ContractorContactID)%>
    <%: Html.CodeMasterDropDownListFor(r => r.ContactType)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Value)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Note)%>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "ContractorContactDetail", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divContractorContactDetail_" + Model.ID.ToString(), Url.Content("~/ContractorContactDetail/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "ContractorContactDetail", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divContractorContactDetail_" + Model.ID.ToString(), Url.Content("~/ContractorContactDetail/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["ContractorContactDetailSaveTemplate"].ToString().Replace("contractorContactDetailID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["ContractorContactDetailCancelTemplate"].ToString().Replace("contractorContactDetailID", Model.ID.ToString()))%>
</td>
