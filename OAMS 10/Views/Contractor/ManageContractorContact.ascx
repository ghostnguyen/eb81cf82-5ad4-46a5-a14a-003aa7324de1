<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Contractor>" %>
<script type="text/javascript" language="javascript">
    function AddContractorContactDetail(contractorContactID) {
        $.ajax({
            url: '<%= Url.Content("~/ContractorContactDetail/Add") %>', type: "GET",
            data: { contractorContactID: contractorContactID },
            success: function (data) {
                $("#divManageContractorContactDetailList" + contractorContactID).append(data);
            }
        });
    }
</script>
<div id="divManageContractorContact">
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
        <tbody id="divManageContractorContactList">
            <% 
                Session["ContractorContactEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(contractorContactID,'divContractorContact_contractorContactID','{0}');", Url.Content("~/ContractorContact/Edit")) } }, false);
                Session["ContractorContactDeleteTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete2(contractorContactID,'divContractorContact_contractorContactID','{0}','divSubContractorContact_contractorContactID');", Url.Content("~/ContractorContact/Delete")) } }, true);
                Session["ContractorContactSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divContractorContact_contractorContactID','{0}');", Url.Content("~/ContractorContact/Edit")) } }, true);
                Session["ContractorContactCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(contractorContactID,'divContractorContact_contractorContactID','{0}');", Url.Content("~/ContractorContact/View")) } }, false);
            %>
            <% foreach (var item in Model.ContractorContacts)
               { %>
            <% Html.RenderPartial("~/Views/ContractorContact/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%--<%: Html.ActionLink("Add", "Add", "ContractorContact", new { href = string.Format("javascript:AddContractorContact({0})", Model.ID) })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractorContactController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddContractorContact({0})", Model.ID) } }, false)%>
    </div>
    <script type="text/javascript" language="javascript">
        function AddContractorContact(contractorID) {
            $.ajax({
                url: '<%= Url.Content("~/ContractorContact/Add") %>', type: "GET",
                data: { contractorID: contractorID },
                success: function (data) {
                    $("#divManageContractorContactList").append(data);
                }
            })
        }
    </script>
</div>
