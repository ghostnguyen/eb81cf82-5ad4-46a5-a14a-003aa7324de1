<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.ClientContact>" %>
<% if (Request.HttpMethod == "GET")
   { %>
<tr id='<%= "divClientContact_" + Model.ID.ToString() %>'>
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
        <%--<%: Html.ActionLink("Edit", "Edit", "ClientContact", new { href = string.Format("javascript:AjaxEdit({0},'{1}','{2}');", Model.ID, "divClientContact_" + Model.ID.ToString(), Url.Content("~/ClientContact/Edit")) })%>
        |
        <%: Html.ActionLink("Delete", "Delete", "ClientContact", new { href = string.Format("javascript:AjaxDelete2({0},'{1}','{2}','{3}');", Model.ID, "divClientContact_" + Model.ID.ToString(), Url.Content("~/ClientContact/Delete"), "divSubClientContact_" + Model.ID.ToString()) })%>--%>
        <%: MvcHtmlString.Create(Session["ClientContactEditTemplate"].ToString().Replace("clientContactID", Model.ID.ToString()))%>
        |
        <%: MvcHtmlString.Create(Session["ClientContactDeleteTemplate"].ToString().Replace("clientContactID", Model.ID.ToString()))%>
    </td>
    <% if (Request.HttpMethod == "GET")
       { %>
</tr>
<tr id='<%= "divSubClientContact_" + Model.ID.ToString() %>'>
    <td colspan="4" style="padding-left:50px;">
        <% Html.RenderPartial("~/Views/ClientContact/ManageClientContactDetail.ascx", Model); %>
        <br />
    </td>
</tr>
<%} %>