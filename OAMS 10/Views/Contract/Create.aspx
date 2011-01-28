<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Contract>" %>

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
            <%: Html.LabelFor(model => model.Number) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Number) %>
            <%: Html.ValidationMessageFor(model => model.Number) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ContractType) %>
        </div>
        <div class="editor-field">
            <%: Html.CodeMasterDropDownListFor(model => model.ContractType) %>
            <%: Html.ValidationMessageFor(model => model.ContractType) %>
        </div>
     
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ContractorName)%>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ContractorName, "AutoCompleteContractor")%>
            <%: Html.ValidationMessageFor(r => r.ContractorName)%>
            <%: Html.TextBoxFor(model => model.ContractorID, new { @style = "visibility:collapse;" })%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ClientName)%>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ClientName, "AutoCompleteClient")%>
            <%: Html.ValidationMessageFor(r => r.ClientName)%>
            <%: Html.TextBoxFor(model => model.ClientID, new { @style = "visibility:collapse;" })%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SignedDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.SignedDate) %>
            <%: Html.ValidationMessageFor(model => model.SignedDate) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ExpiredDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ExpiredDate)%>
            <%: Html.ValidationMessageFor(model => model.ExpiredDate)%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.InspectionFrequency) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.InspectionFrequency) %>
            <%: Html.ValidationMessageFor(model => model.InspectionFrequency) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ReportOn) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ReportOn) %>
            <%: Html.ValidationMessageFor(model => model.ReportOn) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Value) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Value) %>
            <%: Html.ValidationMessageFor(model => model.Value) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.PaymentTerm) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.PaymentTerm) %>
            <%: Html.ValidationMessageFor(model => model.PaymentTerm) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ContactName1) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ContactName1) %>
            <%: Html.ValidationMessageFor(model => model.ContactName1) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Phone1) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Phone1) %>
            <%: Html.ValidationMessageFor(model => model.Phone1) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email1) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Email1) %>
            <%: Html.ValidationMessageFor(model => model.Email1) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.ContractName2) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ContractName2) %>
            <%: Html.ValidationMessageFor(model => model.ContractName2) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Phone2) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Phone2) %>
            <%: Html.ValidationMessageFor(model => model.Phone2) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email2) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Email2) %>
            <%: Html.ValidationMessageFor(model => model.Email2) %>
        </div>
        <p>
            <%--<input type="submit" value="Create" />--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Create", r => r.Create(null), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
</asp:Content>
