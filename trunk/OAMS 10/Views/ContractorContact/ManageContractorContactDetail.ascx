<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractorContact>" %>
<div id='<%= "divManageContractorContactDetail" + Model.ID.ToString()%>'>
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
        <tbody id='<%= "divManageContractorContactDetailList" + Model.ID.ToString()%>'>
            <% 
                Session["ContractorContactDetailEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactDetailController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(contractorContactDetailID,'divContractorContactDetail_contractorContactDetailID','{0}');", Url.Content("~/ContractorContactDetail/Edit")) } }, false);
                Session["ContractorContactDetailDeleteTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactDetailController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete(contractorContactDetailID,'divContractorContactDetail_contractorContactDetailID','{0}');", Url.Content("~/ContractorContactDetail/Delete")) } }, true);
                Session["ContractorContactDetailSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactDetailController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divContractorContactDetail_contractorContactDetailID','{0}');", Url.Content("~/ContractorContactDetail/Edit")) } }, true);
                Session["ContractorContactDetailCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactDetailController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(contractorContactDetailID,'divContractorContactDetail_contractorContactDetailID','{0}');", Url.Content("~/ContractorContactDetail/View")) } }, false);
            %>
            <% foreach (var item in Model.ContractorContactDetails)
               { %>
            <% Html.RenderPartial("~/Views/ContractorContactDetail/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%--<%: Html.ActionLink("Add", "Add", "ContractorContactDetail", new { href = string.Format("javascript:AddContractorContactDetail({0})", Model.ID) })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactDetailController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddContractorContactDetail({0})", Model.ID) } }, false)%>
    </div>
</div>
