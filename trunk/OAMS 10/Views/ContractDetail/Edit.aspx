<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.ContractDetail>" %>

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
            <%: Html.LabelFor(model => model.ContractID) %>
        </div>
        <div class="editor-field">
            <%: Html.DisplayFor(model => model.ContractID) %>
            <%: Html.ValidationMessageFor(model => model.ContractID) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Price) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Price, String.Format("{0:c}", Model.Price)) %>
            <%: Html.ValidationMessageFor(model => model.Price) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ProductionPrice) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ProductionPrice, String.Format("{0:c}", Model.ProductionPrice)) %>
            <%: Html.ValidationMessageFor(model => model.ProductionPrice) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.CurrentProductName)%>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.CurrentProductName, "AutoCompleteProduct")%>
            <%: Html.ValidationMessageFor(r => r.CurrentProductName)%>
            <%: Html.TextBoxFor(model => model.ProductID, new { @style = "visibility:collapse;" })%>
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
            <%: Html.EditorFor(model => model.TermDate) %>
            <%: Html.ValidationMessageFor(model => model.TermDate) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SiteID) %>
        </div>
        <div class="editor-field">
            <%: Html.DisplayFor(model => model.SiteID)%>
            <%: Html.ValidationMessageFor(model => model.SiteID) %>
        </div>
        <div>
            <% Html.RenderPartial("ManageContractDetailTimeline", Model); %>
        </div>
        <p>
            <%--<input type="submit" value="Save" />--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractDetailController>("Save", r => r.Edit(0), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
</asp:Content>
