<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ClientContact>" %>
<div id='<%= "divManageClientContactDetail" + Model.ID.ToString()%>'>
    <table>
        <thead>
            <tr>
                <td>
                    Contact Type
                </td>
                <td>
                    Value
                </td>
                <td>
                    Note
                </td>
                <td>
                    Action
                </td>
            </tr>
        </thead>
        <tbody id='<%= "divManageClientContactDetailList" + Model.ID.ToString()%>'>
            <% 
                Session["ClientContactDetailEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Edit")) } }, false);
                Session["ClientContactDetailDeleteTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Delete")) } }, true);
                Session["ClientContactDetailSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Edit")) } }, true);
                Session["ClientContactDetailCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/View")) } }, false);
            %>
            <%--<% 
                var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Edit")) } }, false);
                var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Delete")) } }, true);
                var saveTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/Edit")) } }, true);
                var cancelTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(clientContactDetailID,'divClientContactDetail_clientContactDetailID','{0}');", Url.Content("~/ClientContactDetail/View")) } }, false);
            %>--%>
            <% foreach (var item in Model.ClientContactDetails)
               { %>
            <%--<% Html.RenderPartial("~/Views/ClientContactDetail/View.ascx", item, new ViewDataDictionary() { { "ClientContactDetailEditTemplate", editTemplate }, { "ClientContactDetailDeleteTemplate", deleteTemplate }, { "ClientContactDetailSaveTemplate", saveTemplate }, { "ClientContactDetailCancelTemplate", cancelTemplate } }); %>--%>
            <% Html.RenderPartial("~/Views/ClientContactDetail/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%--<%: Html.ActionLink("Add", "Add", "ClientContactDetail", new { href = string.Format("javascript:AddClientContactDetail({0})", Model.ID) })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ClientContactDetailController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddClientContactDetail({0})", Model.ID) } }, false)%>
    </div>
</div>
