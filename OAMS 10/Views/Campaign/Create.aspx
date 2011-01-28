<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Campaign>" %>

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
            <%: Html.LabelFor(model => model.ClientID) %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownListFor(model => model.ClientID, (new OAMS.Models.ClientRepository()).GetAll().ToSelectListItem() , OAMSSetting.messageL.SelectNone)%>
            <%: Html.ValidationMessageFor(model => model.ClientID) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.EffectiveDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.EffectiveDate) %>
            <%: Html.ValidationMessageFor(model => model.EffectiveDate) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.TermDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.TermDate)%>
            <%: Html.ValidationMessageFor(model => model.TermDate) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Cost) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Cost) %>
            <%: Html.ValidationMessageFor(model => model.Cost) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ProductionCost) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ProductionCost) %>
            <%: Html.ValidationMessageFor(model => model.ProductionCost) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Note) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Note) %>
            <%: Html.ValidationMessageFor(model => model.Note) %>
        </div>
        <p>
            <%--<input type="submit" value="Create" />--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.CampaignController>("Create", r => r.Create(), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.CampaignController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
</asp:Content>
