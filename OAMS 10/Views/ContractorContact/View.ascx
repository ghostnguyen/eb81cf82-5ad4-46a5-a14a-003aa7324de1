<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ContractorContact>" %>
<% if (Request.HttpMethod == "GET")
   { %>
<tr id='<%= "divContractorContact_" + Model.ID.ToString() %>'>
    <%} %>
    <td>
        <%: Model.Tittle!=null?Model.Tittle:"_"%>
    </td>
    <td>
        <%: Model.Name!=null?Model.Name:"_"%>
    </td>
    <td>
        <%: Model.Note!=null?Model.Note:"_"%>
    </td>
    <td>
        <%--<%: Html.ActionLink("Edit", "Edit", "ContractorContact", new { href = string.Format("javascript:AjaxEdit({0},'{1}','{2}');", Model.ID, "divContractorContact_" + Model.ID.ToString(), Url.Content("~/ContractorContact/Edit")) })%>
        |
        <%: Html.ActionLink("Delete", "Delete", "ContractorContact", new { href = string.Format("javascript:AjaxDelete2({0},'{1}','{2}','{3}');", Model.ID, "divContractorContact_" + Model.ID.ToString(), Url.Content("~/ContractorContact/Delete"), "divSubContractorContact_" + Model.ID.ToString()) })%>--%>
        <%: MvcHtmlString.Create(Session["ContractorContactEditTemplate"].ToString().Replace("contractorContactID", Model.ID.ToString()))%>
        |
        <%: MvcHtmlString.Create(Session["ContractorContactDeleteTemplate"].ToString().Replace("contractorContactID", Model.ID.ToString()))%>
    </td>
    <% if (Request.HttpMethod == "GET")
       { %>
</tr>
<tr id='<%= "divSubContractorContact_" + Model.ID.ToString() %>'>
    <td colspan="4" style="padding-left:50px;">
        <% Html.RenderPartial("~/Views/ContractorContact/ManageContractorContactDetail.ascx", Model); %>
        <br />
    </td>
</tr>
<%} %>