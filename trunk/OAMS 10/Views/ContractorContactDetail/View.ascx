<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractorContactDetail>" %>
<% if (Request.HttpMethod == "GET")
   { %>
<tr id='<%= "divContractorContactDetail_" + Model.ID.ToString() %>'>
    <%} %>
    <td>
        <%: Model.ContactType!=null?Model.ContactType:"_"%>
    </td>
    <td>
        <%: Model.Value != null ? Model.Value : "_"%>
    </td>
    <td>
        <%: Model.Note != null ? Model.Note : "_"%>
    </td>
    <td>
        <%--<%: Html.ActionLink("Edit", "Edit", "ContractorContactDetail", new { href = string.Format("javascript:AjaxEdit({0},'{1}','{2}');", Model.ID, "divContractorContactDetail_" + Model.ID.ToString(), Url.Content("~/ContractorContactDetail/Edit")) })%>
        |
        <%: Html.ActionLink("Delete", "Delete", "ContractorContactDetail", new { href = string.Format("javascript:AjaxDelete({0},'{1}','{2}');", Model.ID, "divContractorContactDetail_" + Model.ID.ToString(), Url.Content("~/ContractorContactDetail/Delete")) })%>--%>
        <%: MvcHtmlString.Create(Session["ContractorContactDetailEditTemplate"].ToString().Replace("contractorContactDetailID", Model.ID.ToString()))%>
        |
        <%: MvcHtmlString.Create(Session["ContractorContactDetailDeleteTemplate"].ToString().Replace("contractorContactDetailID", Model.ID.ToString()))%>
    </td>
    <% if (Request.HttpMethod == "GET")
       { %>
</tr>
<%} %>
