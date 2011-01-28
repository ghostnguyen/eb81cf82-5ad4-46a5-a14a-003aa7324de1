<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Client>" %>

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
            <%: Html.LabelFor(model => model.Fullname_En) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Fullname_En)%>
            <%: Html.ValidationMessageFor(model => model.Fullname_En)%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Fullname_Vi) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Fullname_Vi)%>
            <%: Html.ValidationMessageFor(model => model.Fullname_Vi)%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Address) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Address) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Phone) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Phone) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Fax) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Fax) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Email) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Note) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Note) %>
        </div>
        <p>
            <!--<input type="submit" value="Create" />-->
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Create", r => r.Create(), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
</asp:Content>
