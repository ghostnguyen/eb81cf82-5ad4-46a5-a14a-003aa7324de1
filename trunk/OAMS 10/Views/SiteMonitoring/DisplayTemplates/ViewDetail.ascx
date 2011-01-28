<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteMonitoring>" %>
<table class="notable">
    <tr>
        <td>
            <h1>
                Inspection Report</h1>
        </td>
        <td>
            <img src="<%= Url.Content("~/Content/Image/ambient.png")%>" alt="Ambient" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table class="notable">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    Site ID:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.ID %>
                                </td>
                                <td>
                                    Site Monitoring ID:
                                </td>
                                <td>
                                    <%: Model.ID %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Site code:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Code %>
                                </td>
                                <td>
                                    Supplier:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Contract != null ? Model.ContractDetail.Contract.ContractorName : Model.Site.ContractorName %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Site location:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.AddressLine1 + " " + Model.ContractDetail.Site.AddressLine2 + " " + (Model.ContractDetail.Site.Geo3 == null ? "" : Model.ContractDetail.Site.Geo3.Name) %>
                                </td>
                                <td>
                                    Current Client:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Contract.ClientName%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Province:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Geo1.Name%>
                                </td>
                                <td>
                                    GPS Lattitude:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Lat %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    District:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Geo2.Name%>
                                </td>
                                <td>
                                    GPS Longitude:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Lng%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="notable">
                            <tr>
                                <td>
                                    <img alt="Map" src="http://maps.google.com/maps/api/staticmap?center=<%: Model.ContractDetail.Site.Lat%>,<%: Model.ContractDetail.Site.Lng%>&zoom=17&size=300x225&maptype=hybrid&markers=color:red|<%: Model.ContractDetail.Site.Lat%>,<%: Model.ContractDetail.Site.Lng%>&sensor=false" />
                                    <%--http://maps.google.com/maps/api/staticmap?center=10.80129,106.71272&zoom=17&size=300x225&maptype=roadmap%20&markers=color:red|10.80129,106.71272&sensor=false--%>
                                    <%--<div id="map" style="width: 300px; height: 225px;">
                                        </div>--%>
                                    <%--<script type="text/javascript" language="javascript">
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
                                                    var lng = $('#ContractDetail_Site_Lng').val();
                                                    var lat = $('#ContractDetail_Site_Lat').val();

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
                                        <%: Html.HiddenFor(model => model.ContractDetail.Site.Lat)%>
                                        <%: Html.HiddenFor(model => model.ContractDetail.Site.Lng)%>--%>
                                </td>
                                <%  int i = 0;
                                    bool hasCloseTr = false;
                                    foreach (var item in Model.SiteMonitoringPhotoes)
                                    { %>
                                <%
                                    if (i % 2 == 0)
                                    { 
                                %>
                                <td valign="top">
                                    <img src='<%= item.Url.ToUrlPicasaPhotoResize() %>' alt="" width="300" height="225" />
                                </td>
                            </tr>
                            <%
                                hasCloseTr = true;
                                    }
                                    else
                                    {
                            %>
                            <tr>
                                <td valign="top">
                                    <img src='<%= item.Url.ToUrlPicasaPhotoResize() %>' alt="" width="300" height="225" />
                                </td>
                                <% 
hasCloseTr = false;
                                    }
                                    i++;
                                %>
                                <% } %>
                                <%
                                    if (!hasCloseTr)
                                    { 
                                %>
                                <td>
                                </td>
                            </tr>
                            <%
                                }
                            %>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table class="notable">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Current Product:
                                </td>
                                <td>
                                    <%: Model.Product == null ? "" : Model.Product.Name %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Advertising Type:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Type %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Format Execution:
                                </td>
                                <td>
                                    <%--<%: Model.ContractDetail.Site.Format %>--%>
                                    <%: Model.ContractDetail.Format %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In/Outbound to CBC:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.CBDViewed %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Size:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Height %>m x
                                    <%: Model.ContractDetail.Site.Width %>m
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Latest Dates</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Latest Site Inspection:
                                </td>
                                <td>
                                    <%: Model.LastUpdatedDate %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Latest Photo Taken:
                                </td>
                                <td>
                                    <%
                                        DateTime? lastDate = null;
                                        foreach (OAMS.Models.SiteMonitoringPhoto item in Model.SiteMonitoringPhotoes)
                                        {
                                            if (item.TakenDate != null && (lastDate == null || lastDate < item.TakenDate))
                                            {
                                                lastDate = item.TakenDate;
                                            }
                                        }
                                        if (lastDate != null)
                                        { 
                                    %>
                                    <%: lastDate.ToShortDateString() %>
                                    <%
}
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Lighting</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Backlit/Frontlit Vendor:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.FrontlitNumerOfLamps>0?"Frontlit":"Backlit" %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Working:
                                </td>
                                <td>
                                    <%: Model.Working.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. of bulbs:
                                </td>
                                <td>
                                    <%: Model.NoOfBullbs%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bulbs working:
                                </td>
                                <td>
                                    <%: Model.BullsWorking%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Issues:
                                </td>
                                <td>
                                    <%: Model.Issues%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Status of ContractDetail.Site</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Clean:
                                </td>
                                <td>
                                    <%: Model.Clean.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Creative Good Condition:
                                </td>
                                <td>
                                    <%: Model.CreativeGoodConditon.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    External Interference:
                                </td>
                                <td>
                                    <%: Model.ExternalInterference.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vandalism:
                                </td>
                                <td>
                                    <%: Model.Vandalism.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Comments:
                                </td>
                                <td>
                                    <%: Model.Comments%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Follow up (if any)</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Required:
                                </td>
                                <td>
                                    <%: Model.RequiredFollowUp.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Action:
                                </td>
                                <td>
                                    <%: Model.Action%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date of problem:
                                </td>
                                <td>
                                    <%: Model.DateOfProblem.ToShortDateString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Now fixed:
                                </td>
                                <td>
                                    <%: Model.NowFixed.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date fixed:
                                </td>
                                <td>
                                    <%: Model.DateFixed.ToShortDateString() %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
