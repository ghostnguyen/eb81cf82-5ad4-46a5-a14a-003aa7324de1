<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteDetail>" %>
<%@ Import Namespace="OAMS.Controllers" %>
<div id='<%= "divManageSiteDetailPhoto" + Model.ID.ToString()%>'>
    <% foreach (var item in Model.SiteDetailPhotoes)
       { %>
    <br />
    <input type="text" value='<%: item.Note %>' id='siteDetailPhotoNote<%: item.ID %>' />
    <input type="button" value="Save Note" onclick="UpdateSitePhotoNote('<%= Url.Content("~/SiteDetailPhoto/EditNote") %>','<%= item.ID %>',$('#siteDetailPhotoNote<%: item.ID %>').val())" />
    <input type="button" value="Delete this image" onclick="deleteSitePhoto(this,'<%= item.ID %>','DeleteSiteDetailPhotoList',$('#divDeleteSiteDetailPhotoList'),$('#siteDetailPhoto<%: item.ID %>'))" />
    <br />
    <% if (!item.IsValidGPS)
       {
    %>
    <span style="color: Red;">Possible wrong GPS. </span>
    <%} %>
    <br />
    <img src='<%= item.Url %>' alt="" width="500" id='siteDetailPhoto<%: item.ID %>' />
    <br />
    <% } %>
    <br />
    <div id='divMoreSiteDetailFile_<%: Model.ID %>'>
    </div>
    <br />
    <input type="button" value="Add more" onclick="addMoreFileInput2('divMoreSiteDetailFile_<%: Model.ID %>','siteDetailFiles',<%: Model.ID %>)" />
</div>
