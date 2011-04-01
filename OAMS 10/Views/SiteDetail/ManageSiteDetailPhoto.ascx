<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<%@ Import Namespace="OAMS.Controllers" %>
<div id='<%= "divManageSiteDetailPhoto" + Model.ID.ToString()%>'>
    <div id="divDeleteSiteDetailPhotoList" style="visibility: collapse;">
    </div>
    <% foreach (var item in Model.SiteDetailPhotoes)
       { %>
    <br />
    <input type="text" value='<%: item.Note %>' id='photoNote<%: item.ID %>' />
    <input type="button" value="Save Note" onclick="UpdateSitePhotoNote('<%= Url.Content("~/SitePhoto/EditNote") %>','<%= item.ID %>',$('#photoNote<%: item.ID %>').val())" />
    <input type="button" value="Delete this image" onclick="deleteSitePhoto(this,'<%= item.ID %>')" />
    <br />
    <%--<% if (!item.IsValidGPS)
       {
    %>
    <span style="color: Red;">Possible wrong GPS. </span>
    <%} %>--%>
    <br />
    <img src='<%= item.Url %>' alt="" width="500" id='photo<%: item.ID %>' />
    <br />
    <% } %>
    <br />
    <div id="divMoreFile">
    </div>
    <br />
    <input type="button" value="Add more" onclick="addMoreFileInput('divMoreFile','files','noteList')" />
</div>
