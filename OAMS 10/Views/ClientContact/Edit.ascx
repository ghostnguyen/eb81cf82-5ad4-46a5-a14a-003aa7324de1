<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ClientContact>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.ClientID)%>
    <%: Html.TextBoxFor(model => model.Tittle)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Name)%>
</td>
<td>
    <%: Html.TextBoxFor(model => model.Note)%>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "ClientContact", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divClientContact_" + Model.ID.ToString(), Url.Content("~/ClientContact/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "ClientContact", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divClientContact_" + Model.ID.ToString(), Url.Content("~/ClientContact/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["ClientContactSaveTemplate"].ToString().Replace("clientContactID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["ClientContactCancelTemplate"].ToString().Replace("clientContactID", Model.ID.ToString()))%>
</td>
