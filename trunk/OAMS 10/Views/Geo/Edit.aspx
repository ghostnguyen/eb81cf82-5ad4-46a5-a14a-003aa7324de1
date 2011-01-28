<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Geo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%: Html.HiddenFor(model => model.ID) %>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>
        <p>
            <%--<input type="submit" value="Save" />--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Save", r => r.Edit(new Guid()), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index", new {parentID = Model.ParentID })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.GeoController>("Back to List", r => r.Index(null), new RouteValueDictionary(new { parentID = Model.ParentID }), null, false)%>
    </div>
</asp:Content>
