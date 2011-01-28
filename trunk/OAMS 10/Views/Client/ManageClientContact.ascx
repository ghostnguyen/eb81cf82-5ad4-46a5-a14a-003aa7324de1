<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Client>" %>
<script type="text/javascript" language="javascript">
    function AddClientContactDetail(clientContactID) {
        $.ajax({
            url: '<%= Url.Content("~/ClientContactDetail/Add") %>', type: "GET",
            data: { clientContactID: clientContactID },
            success: function (data) {
                $("#divManageClientContactDetailList" + clientContactID).append(data);
            }
        });
    }
</script>
<div id="divManageClientContact">
    <table>
        <thead>
            <tr>
                <td>
                    Title
                </td>
                <td>
                    Name
                </td>
                <td>
                    Note
                </td>
                <td>
                    Action
                </td>
            </tr>
        </thead>
        <tbody id="divManageClientContactList">
            <% 
                Session["ClientContactEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(clientContactID,'divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/Edit")) } }, false);
                Session["ClientContactDeleteTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete2(clientContactID,'divClientContact_clientContactID','{0}','divSubClientContact_clientContactID');", Url.Content("~/ClientContact/Delete")) } }, true);
                Session["ClientContactSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/Edit")) } }, true);
                Session["ClientContactCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(clientContactID,'divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/View")) } }, false);
            %>
            <%--<% 
                var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(clientContactID,'divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/Edit")) } }, false);
                var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete2(clientContactID,'divClientContact_clientContactID','{0}','divSubClientContact_clientContactID');", Url.Content("~/ClientContact/Delete")) } }, true);
                var saveTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/Edit")) } }, true);
                var cancelTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(clientContactID,'divClientContact_clientContactID','{0}');", Url.Content("~/ClientContact/View")) } }, false);
            %>--%>
            <% foreach (var item in Model.ClientContacts)
               { %>
            <%--<% Html.RenderPartial("~/Views/ClientContact/View.ascx", item, new ViewDataDictionary() { { "ClientContactEditTemplate", editTemplate }, { "ClientContactDeleteTemplate", deleteTemplate }, { "ClientContactSaveTemplate", saveTemplate }, { "ClientContactCancelTemplate", cancelTemplate } }); %>--%>
            <% Html.RenderPartial("~/Views/ClientContact/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%--<%: Html.ActionLink("Add", "Add", "ClientContact", new { href = string.Format("javascript:AddClientContact({0})", Model.ID) })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddClientContact({0})", Model.ID) } }, false)%>
    </div>
    <script type="text/javascript" language="javascript">
        function AddClientContact(clientID) {
            $.ajax({
                url: '<%= Url.Content("~/ClientContact/Add") %>', type: "GET",
                data: { clientID: clientID },
                success: function (data) {
                    $("#divManageClientContactList").append(data);
                }
            })
        }
    </script>
</div>
