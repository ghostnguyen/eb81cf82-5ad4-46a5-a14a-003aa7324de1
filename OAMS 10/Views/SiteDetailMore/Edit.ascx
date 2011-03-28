<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetailMore>" %>
<%@ Import Namespace="OAMS.Controllers" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.SiteDetailID)%>
</td>
<td>
    <%: Html.EditorFor(model => model.CurrentProductName, "AutoCompleteProduct")%>
    <%: Html.ValidationMessageFor(r => r.CurrentProductName)%>
    <%: Html.TextBoxFor(model => model.ProductID, new { @style = "visibility:collapse;" })%>
</td>
<td>
</td>
<td>
    <%: Html.ActionLinkWithRoles<SiteDetailMoreController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divSiteDetailMore_{0}','{1}');", Model.ID.ToString(), Url.Content("~/SiteDetailMore/Edit")) } }, true) %>
    |
    <%: Html.ActionLinkWithRoles<SiteDetailMoreController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView({0},'divSiteDetailMore_{0}','{1}');", Model.ID.ToString(), Url.Content("~/SiteDetailMore/View")) } }, false)%>
</td>
