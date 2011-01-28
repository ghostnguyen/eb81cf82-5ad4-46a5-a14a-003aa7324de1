<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Client>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
<div style="float:left;width:25%;margin-right:10px;">
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
            <!--<input type="submit" value="Save" />-->
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Save", r => r.Edit(0), null, null, true)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
    <br />
    <div>
        Replace:
        <%: Html.EditorFor(model => model.ReplaceFor, "AutoCompleteClient")%>
        <%--<%: Html.ActionLink("Replace", "Replace")%>--%>
        <%--<input type="button" id="btnReplace" value="Replace" onclick="replace();" />--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientController>("Replace", r => r.Replace(0, 0), null, new Dictionary<string, object>() { { "href", "javascript:replace();" } }, true)%>
        <input id="ClientID" name="ClientID" style="visibility: collapse;" type="text"
            value="" />
    </div>
    <script type="text/javascript">

        function replace() {

            var id = $("#ID").val();
            var replaceID = $("#ClientID").val();

            $.ajax({
                url: '<%= Url.Content("~/Client/Replace") %>', type: "POST", dataType: "json",
                data: { "id": id, "replaceID": replaceID },
                success: function (data) {
                    alert(data);
                }
            });
        }

    </script>
</div>
<div style="float:left;width:73%;">
    <br />
    <div style="clear:both;"><b>Client Contacts:</b></div>
    <% Html.RenderPartial("ManageClientContact", Model); %>
</div>
</asp:Content>
