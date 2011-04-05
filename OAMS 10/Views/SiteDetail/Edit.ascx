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
    <%: MvcHtmlString.Create(Session["SiteDetailSaveTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["SiteDetailCancelTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
</td>
