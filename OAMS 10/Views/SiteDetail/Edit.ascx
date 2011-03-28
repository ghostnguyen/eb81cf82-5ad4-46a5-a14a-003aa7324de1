<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.SiteID) %>
    <%: Html.TextBoxFor(model => model.Name) %>
</td>
<td>
    <%: Html.CodeMasterDropDownListFor(r => r.Type)%>
</td>
<td>
    <%: Html.CodeMasterDropDownListFor(r => r.Format)%>
</td>
<td>
    <%: Html.EditorFor(r => r.Height)%>
</td>
<td>
    <%: Html.EditorFor(r => r.Width)%>
</td>
<td>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "SiteDetail", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "SiteDetail", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["SiteDetailSaveTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["SiteDetailCancelTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
</td>
