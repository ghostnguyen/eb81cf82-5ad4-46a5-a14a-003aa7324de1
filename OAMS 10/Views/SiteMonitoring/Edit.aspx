<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.SiteMonitoring>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm("Edit", "SiteMonitoring", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <%: Html.ValidationSummary(true) %>
    <table>
        <tr valign="top">
            <td>
                <%--<%: Html.ActionLink("View Report", "View", new { id = Model.ID })%>--%>
                <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("View Report", r => r.View(0), new RouteValueDictionary(new { id = Model.ID }), null, false)%>
                <fieldset>
                    <legend>Fields</legend>
                    <%: Html.HiddenFor(model => model.ID) %>
                    <%: Html.HiddenFor(model => model.ContractDetailID) %>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Order)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.DropDownListForSiteMonitoringOrder(r => r.Order,Model) %>
                    </div>
                    <%--<div class="editor-field">
                        <% 
                            var timeline = Model.ContractDetail.ContractDetailTimelines.Where(r => r.Order == Model.Order).FirstOrDefault();
                            if (timeline == null) timeline = new OAMS.Models.ContractDetailTimeline();
                            Html.RenderPartial("~/Views/ContractDetailTimeline/ViewOnly.ascx", timeline); 
                        %>
                    </div>--%>
                    <div id="map" style="width: 300px; height: 300px;">
                    </div>
                    <script type="text/javascript" language="javascript">
                        var map;
                        var marker;
                        var first = true;
                        function init() {
                            var mapDiv = document.getElementById('map');
                            map = new google.maps.Map(mapDiv, {
                                center: new google.maps.LatLng(10.77250, 106.69808),
                                zoom: 17,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            });
                            google.maps.event.addListener(map, 'idle', MapIdle);
                        }

                        google.maps.event.addDomListener(window, 'load', init);

                        function MapIdle() {
                            if (first) {
                                var lng = $('#Site_Lng').val();
                                var lat = $('#Site_Lat').val();

                                if (marker != null) {
                                    marker.setMap(null);
                                }

                                marker = new google.maps.Marker({

                                    position: new google.maps.LatLng(lat, lng),
                                    map: map,
                                    draggable: true,
                                    title: 'Move me!'
                                });

                                if (!map.getBounds().contains(marker.position)) {
                                    var bounds = new google.maps.LatLngBounds();
                                    bounds.extend(marker.position);
                                    map.fitBounds(bounds);
                                    map.setZoom(17);
                                }
                                first = false;
                            }
                        }
                    </script>
                    <div class="editor-label">
                        Address
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.Site.AddressLine1)%>&nbsp;<%: Html.DisplayFor(model => model.Site.AddressLine2)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Site.GeoFullName)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.Site.GeoFullName)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.CurrentProductName)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.CurrentProductName, "AutoCompleteProduct")%>
                        <%: Html.ValidationMessageFor(r => r.CurrentProductName)%>
                        <%: Html.TextBoxFor(model => model.ProductID, new { @style = "visibility:collapse;" })%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Working) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.Working) %>
                        <%: Html.ValidationMessageFor(model => model.Working) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.NoOfBullbs) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.NoOfBullbs) %>
                        <%: Html.ValidationMessageFor(model => model.NoOfBullbs) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.BullsWorking) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.BullsWorking) %>
                        <%: Html.ValidationMessageFor(model => model.BullsWorking) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.IssuesCount) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.IssuesCount)%>
                        <%: Html.ValidationMessageFor(model => model.IssuesCount)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Issues) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Issues) %>
                        <%: Html.ValidationMessageFor(model => model.Issues) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Clean) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.Clean)%>
                        <%: Html.ValidationMessageFor(model => model.Clean) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.CreativeGoodConditon) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.CreativeGoodConditon)%>
                        <%: Html.ValidationMessageFor(model => model.CreativeGoodConditon) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.ExternalInterference) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.ExternalInterference)%>
                        <%: Html.ValidationMessageFor(model => model.ExternalInterference) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Vandalism) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.Vandalism)%>
                        <%: Html.ValidationMessageFor(model => model.Vandalism) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Comments) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Comments) %>
                        <%: Html.ValidationMessageFor(model => model.Comments) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.RequiredFollowUp) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.RequiredFollowUp)%>
                        <%: Html.ValidationMessageFor(model => model.RequiredFollowUp) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Action) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Action) %>
                        <%: Html.ValidationMessageFor(model => model.Action) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.DateOfProblem) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.DateOfProblem)%>
                        <%: Html.ValidationMessageFor(model => model.DateOfProblem) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.NowFixed) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.NowFixed)%>
                        <%: Html.ValidationMessageFor(model => model.NowFixed) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.DateFixed) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.EditorFor(model => model.DateFixed)%>
                        <%: Html.ValidationMessageFor(model => model.DateFixed) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.CreatedDate) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.CreatedDate) %>
                        <%: Html.ValidationMessageFor(model => model.CreatedDate) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.CreatedBy) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.CreatedBy)%>
                        <%: Html.ValidationMessageFor(model => model.CreatedBy) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.LastUpdatedDate) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.LastUpdatedDate)%>
                        <%: Html.ValidationMessageFor(model => model.LastUpdatedDate) %>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.LastUpdatedBy) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.LastUpdatedBy)%>
                        <%: Html.ValidationMessageFor(model => model.LastUpdatedBy) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.HiddenFor(model => model.Site.Lat)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.HiddenFor(model => model.Site.Lng)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Site.Type) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(r => r.Site.Type)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.ContractDetail.Format)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(r => r.ContractDetail.Format)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Site.Height)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.Site.Height)%>
                    </div>
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Site.Width) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.DisplayFor(model => model.Site.Width)%>
                    </div>
                    <p>
                        <%--<input type="submit" value="Save" />--%>
                        <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("Save", r => r.Edit(0), null, null, true)%>
                        <%--<%: Html.ActionLink("View Report", "View", new { id = Model.ID })%>--%>
                        <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("View Report", r => r.View(0), new RouteValueDictionary(new { id = Model.ID }), null, false)%>
                    </p>
                </fieldset>
            </td>
            <td>
                <% 
                    string albumUrl = Model.ContractDetail.Site.AlbumUrl;
                    string AlbumID = string.IsNullOrEmpty(albumUrl) ? "" : Model.ContractDetail.Site.AlbumUrl.Split('/')[9].Split('?')[0];
                    string AuthID = string.IsNullOrEmpty(albumUrl) ? "" : albumUrl.Split('?')[1].Split('=')[1];
                %>
                <embed type="application/x-shockwave-flash" src="http://picasaweb.google.com/s/c/bin/slideshow.swf"
                    width="600" height="400" flashvars="host=picasaweb.google.com&hl=en_US&feat=flashalbum&RGB=0x000000&feed=http%3A%2F%2Fpicasaweb.google.com%2Fdata%2Ffeed%2Fapi%2Fuser%2F113917932111131696693%2Falbumid%2F<%=AlbumID%>%3Falt%3Drss%26kind%3Dphoto%26authkey%3D<%=AuthID%>%26hl%3Den_US"
                    pluginspage="http://www.macromedia.com/go/getflashplayer"></embed>
                <br />
                <div id="divDeletePhotoList" style="visibility: collapse;">
                    <%--<input type="text" id="Text1" name="DeletePhotoList" />--%>
                </div>
                <% 
                    foreach (var item in Model.SiteMonitoringPhotoes)
                    { %>
                <br />
                <input type="text" value='<%: item.Note %>' id='photoNote<%: item.ID %>' />
                <input type="button" value="Save Note" onclick="UpdateSiteMonitoringPhotoNote('<%= Url.Content("~/SiteMonitoringPhoto/EditNote") %>','<%= item.ID %>',$('#photoNote<%: item.ID %>').val())" />
                <input type="button" value="Delete this image" onclick="deleteSitePhoto(this,'<%= item.ID %>')" />
                <br />
                <% if (!item.IsValidTakenDate)
                   {
                %>
                <span style="color: Red;">Wrong photo taken date.</span>
                <%} %>
                <br />
                <img src='<%= item.Url %>' alt="" width="500" id='photo<%: item.ID %>' />
                <br />
                <% } %>
                <br />
                <%--<label for="file1">
                    Filename:</label>
                <input type="file" name="files" id="file3" size="65" onchange="preview(this, 3)"/>
                <br />
                <img alt="Graphic will preview here" id="previewField3"/>--%>
                <div id="divMoreFile">
                </div>
                <br />
                <input type="button" value="Add more" onclick="addMoreFileInput('divMoreFile','files','noteList')" />
                <br />
                <fieldset>
                    <legend>Add fixed photos.</legend>
                    <div id="divMoreFileOfFixed">
                    </div>
                    <br />
                    <input type="button" value="Add more" onclick="addMoreFileInput('divMoreFileOfFixed','filesOfFixed','noteOfFixedList')" />
                </fieldset>
            </td>
        </tr>
    </table>
    <% } %>
    <div>
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%--<%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("Back to List", r => r.Index(), null, null, false)%>--%>
    </div>
</asp:Content>
