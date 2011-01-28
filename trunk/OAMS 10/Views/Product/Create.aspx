<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Note) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Note) %>
            <%: Html.ValidationMessageFor(model => model.Note) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.CategoryID1) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.NewCategoryFullName, "AutoCompleteCategory")%>
        </div>
        <div class="editor-label">
            Client
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.NewClientName, "AutoCompleteClient")%>
            <%: Html.ValidationMessageFor(r => r.NewClientName)%>
            <%: Html.TextBoxFor(model => model.ClientID, new { @style = "visibility:collapse;" })%>
        </div>
        <p>
            <%--<input type="submit" value="Create" />--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Create", r => r.Create(null), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ProductController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
</asp:Content>
