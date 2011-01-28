<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ClientContactDetail>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.ClientContactID)%>
    <%: Html.CodeMasterDropDownListFor(r => r.ContactType)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Value)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Note)%>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "ClientContactDetail", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divClientContactDetail_" + Model.ID.ToString(), Url.Content("~/ClientContactDetail/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "ClientContactDetail", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divClientContactDetail_" + Model.ID.ToString(), Url.Content("~/ClientContactDetail/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["ClientContactDetailSaveTemplate"].ToString().Replace("clientContactDetailID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["ClientContactDetailCancelTemplate"].ToString().Replace("clientContactDetailID", Model.ID.ToString()))%>
</td>
