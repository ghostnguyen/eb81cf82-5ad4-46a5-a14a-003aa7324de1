<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<td>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.SiteID) %>
    <%: Html.TextBoxFor(model => model.Name) %>
</td>
<td>
    <%: Html.CodeMasterDropDownListFor(r => r.Format)%>
</td>
<td>
    <%--<%: Html.EditorFor(model => model.CurrentClientName, "AutoCompleteClient")%>
    <%: Html.ValidationMessageFor(r => r.CurrentClientName)%>
    <%: Html.TextBoxFor(model => model.ClientID, new { @style = "visibility:collapse;" })%>--%>
</td>
<td>
    <%: Html.EditorFor(model => model.CurrentProductName, "AutoCompleteProduct")%>
    <%: Html.ValidationMessageFor(r => r.CurrentProductName)%>
    <%: Html.TextBoxFor(model => model.ProductID, new { @style = "visibility:collapse;" })%>																
</td>
<td>
    <%--<%: Html.EditorFor(model => model.NewCategoryFullName, "AutoCompleteCategory")%>--%>
</td>
<td>
    <%--<%: Html.ActionLink("Save", "Edit", "SiteDetail", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/Edit")) })%>
    |
    <%: Html.ActionLink("Cancel", "View", "SiteDetail", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/View")) })%>--%>
    <%: MvcHtmlString.Create(Session["SiteDetailSaveTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
    |
    <%: MvcHtmlString.Create(Session["SiteDetailCancelTemplate"].ToString().Replace("siteDetailID", Model.ID.ToString()))%>
</td>
<%--<%: Html.ValidationSummary(true) %>
<fieldset>
    <legend>Fields</legend>
    <%: Html.HiddenFor(model => model.ID) %>
    <%: Html.HiddenFor(model => model.SiteID) %>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Name) %>
        <%: Html.ValidationMessageFor(model => model.Name) %>
    </div>
    <div class="editor-label">
        CurrentClient
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.CurrentClientName, "AutoCompleteClient")%>
        <%: Html.ValidationMessageFor(r => r.CurrentClientName)%>
        <%: Html.TextBoxFor(model => model.ClientID, new { @style = "visibility:collapse;" })%>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.NewCategoryFullName)%>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.NewCategoryFullName, "AutoCompleteCategory")%>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Product) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Product) %>
        <%: Html.ValidationMessageFor(model => model.Product) %>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Format) %>
    </div>
    <div class="editor-field">
        <%: Html.CodeMasterDropDownListFor(r => r.Format)%>
        <%: Html.ValidationMessageFor(model => model.Format)%>
    </div>
    <p>
        <%: Html.ActionLink("Save", "Edit", "SiteDetail", new { href = string.Format("javascript:AjaxSave('{0}','{1}');", "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/Edit")) })%>
        |
        <%: Html.ActionLink("Cancel", "View", "SiteDetail", new { href = string.Format("javascript:AjaxView({0},'{1}','{2}');", Model.ID, "divSiteDetail_" + Model.ID.ToString(), Url.Content("~/SiteDetail/View")) })%>
    </p>
</fieldset>
--%>